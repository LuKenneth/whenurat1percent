using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {

	public GameObject blueBubbleLower;
	public GameObject grayBubbleUpper;
	public GameObject theirMessageUpper;
	public GameObject playerMessageLower;

	public GameObject blueBubbleUpper;
	public GameObject grayBubbleLower;
	public GameObject theirMessageLower;
	public GameObject playerMessageUpper;

	public Timer timer;
	public GameObject powerDown;
	private float gameTime = 20.0f;
	public bool isFirstReply = true;

	public GameObject[] replies;
	private TheirMessage tm;
	private PlayerMessage pm;
	private PlayerMessage[] pms;
	public AudioSource outgoing;
	public AudioSource incoming;

	public GameObject gameOverAlert;
	public GameObject youWinAlert;
	public GameObject lowPowerMode;
	public GameObject read;
	public string lorMessage;

	public GameObject middleFinger;
	public GameObject drakeJosh;
	public GameObject nsfw;

	// Use this for initialization
	void Start () {
		initializeMessages ();
		theirMessageUpper.GetComponent<TextMesh>().text = tm.message;

	}
	
	// Update is called once per frame
	void Update () {
		gameTime -= Time.deltaTime;
		if(gameTime <= 0.0f) {
			shutDown();
		}
	}

	void shutDown() {
		powerDown.SetActive(true);
		for (int i = 0; i < replies.Length; i++) {
			replies [i].SetActive (false);
			replies [i].GetComponent<TextMesh> ().text = "";
		}
	}

	public void reply(Reply reply) {
		for (int i = 0; i < replies.Length; i++) {
			replies [i].SetActive (false);
			replies [i].GetComponent<TextMesh> ().text = "";
		}
		
		outgoing.Play ();
		pm = reply.pm;
		if (isFirstReply) {
			playerMessageLower.GetComponent<TextMesh> ().text = reply.pm.message;
			playerMessageLower.SetActive (true);
			blueBubbleLower.SetActive (true);
			for (int i = 0; i < replies.Length; i++) {
				replies [i].SetActive (false);
				replies [i].GetComponent<TextMesh> ().text = "";
			}
		} else {
			rotateMessages ();

		}
		playerMessageUpper.GetComponent<TextMesh> ().text = pm.message;
		playerMessageLower.GetComponent<TextMesh> ().text = pm.message;
		theirMessageLower.GetComponent<TextMesh> ().text = tm.message;

		if (!reply.pm.leftOnRead) {
			timer.RunAfter (nextMessage, 0.5f);
		} else {

			if (!reply.pm.altEnding) {
				lorMessage = pm.lorMessage;
				timer.RunAfter (showRead, 1.0f);
			} else {
				rotateMessages ();
				lorMessage = pm.lorMessage;
				theirMessageLower.SetActive (false);
				theirMessageUpper.SetActive (false);
				grayBubbleLower.SetActive (false);
				grayBubbleUpper.SetActive (false);
				if (reply.pm.drakeJosh) {
					drakeJosh.SetActive (true);
				}
				if (reply.pm.middleFinger) {
					middleFinger.SetActive (true);
				}
				timer.RunAfter (gameOver, 2.0f);
			}
		}
	}

	public void showRead() {
		read.SetActive (true);
		timer.RunAfter (gameOver, 2.0f);
	}

	public void gameOver() {
		theirMessageLower.SetActive (false);
		theirMessageUpper.SetActive (false);
		playerMessageLower.SetActive (false);
		playerMessageUpper.SetActive (false);
		gameOverAlert.SetActive (true);
		gameOverAlert.GetComponentInChildren<TextMesh> ().text = lorMessage;
		for (int i = 0; i < replies.Length; i++) {
			replies [i].SetActive (false);
			replies [i].GetComponent<TextMesh> ().text = "";
		}
	}

	public void nextMessage() {
		timer.run = false;
		theirMessageLower.GetComponent<TextMesh> ().text = pm.herReply.message;
		theirMessageUpper.GetComponent<TextMesh> ().text = pm.herReply.message;
		rotateMessages ();
		timer.RunAfter (incomingMessage, 0.5f);
	}

	public void newReplies() {
		for (int i = 0; i < replies.Length; i++) {
			replies [i].SetActive (true);
			replies [i].GetComponent<TextMesh> ().text = tm.replies [i].message;
			replies [i].GetComponent<Reply> ().pm = tm.replies [i];
		}
	}

	public void rotateMessages() {
		playerMessageLower.SetActive (!playerMessageLower.activeSelf);
		playerMessageUpper.SetActive (!playerMessageUpper.activeSelf);
		blueBubbleLower.SetActive (playerMessageLower.activeSelf);
		blueBubbleUpper.SetActive (playerMessageUpper.activeSelf);
		if (isFirstReply) {
			grayBubbleUpper.SetActive (false);
			theirMessageUpper.SetActive (false);
			isFirstReply = false;
		} else {
			grayBubbleUpper.SetActive (!grayBubbleUpper.activeSelf);
			theirMessageUpper.SetActive (!theirMessageUpper.activeSelf);
			grayBubbleLower.SetActive (!grayBubbleLower.activeSelf);
			theirMessageLower.SetActive (!theirMessageLower.activeSelf);
		}
	}
	
	public void incomingMessage() {
		incoming.Play ();
		timer.run = false;
		tm = pm.herReply;
		if (!tm.gameOver && !tm.youWin) {
			
			grayBubbleLower.SetActive (true);
			theirMessageLower.SetActive (true);
			newReplies ();
		}
		if (tm.gameOver) {
			theirMessageLower.SetActive (false);
			theirMessageUpper.SetActive (false);
			playerMessageLower.SetActive (false);
			playerMessageUpper.SetActive (false);
			gameOverAlert.SetActive (true);
			gameOverAlert.GetComponentInChildren<TextMesh> ().text = tm.gameOverMessage;
			for (int i = 0; i < replies.Length; i++) {
				replies [i].SetActive (false);
				replies [i].GetComponent<TextMesh> ().text = "";
			}
		}
		if (tm.youWin) {
			for (int i = 0; i < replies.Length; i++) {
				replies [i].SetActive (false);
				replies [i].GetComponent<TextMesh> ().text = "";
			}
			if (!tm.nsfw) {
				timer.RunAfter (showYouWin, 2.0f);

			} else {
				theirMessageUpper.SetActive (true);
				theirMessageUpper.GetComponent<TextMesh> ().text = tm.message;
				grayBubbleLower.SetActive (false);
				grayBubbleUpper.SetActive (true);
				theirMessageLower.SetActive (false);
				playerMessageUpper.SetActive (false);
				blueBubbleUpper.SetActive (false);
				nsfw.SetActive (true);
				timer.RunAfter (showYouWin, 2.0f);
			}
		}
	}

	public void showYouWin() {
		theirMessageLower.SetActive (false);
		theirMessageUpper.SetActive (false);
		playerMessageLower.SetActive (false);
		playerMessageUpper.SetActive (false);
		youWinAlert.SetActive (true);
		youWinAlert.GetComponentInChildren<TextMesh> ().text = tm.gameOverMessage;
	}

	public void restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void initializeMessages() {

		TheirMessage first = new TheirMessage ("Heyyy");
			PlayerMessage hey = new PlayerMessage ("Hey!");
				TheirMessage wyd = new TheirMessage ("wyd?");
				hey.herReply = wyd;
					PlayerMessage creatingNudes = new PlayerMessage ("Creating Nudes");
						creatingNudes.leftOnRead = true;
						creatingNudes.lorMessage = "That was a little weird, \ntry again";
					PlayerMessage jChilling = new PlayerMessage ("j chillin");
						TheirMessage cool = new TheirMessage ("Cool!");
						jChilling.herReply = cool;		
							PlayerMessage yeah = new PlayerMessage ("yeah"); 
								yeah.leftOnRead = true;
								yeah.lorMessage = "Too boring.";
							PlayerMessage yeah2 = new PlayerMessage ("yeah");
								yeah2.leftOnRead = true;
								yeah2.lorMessage = "Too boring.";
							PlayerMessage yeah3 = new PlayerMessage ("yeah");
								yeah3.leftOnRead = true;
								yeah3.lorMessage = "Too boring.";
						cool.setReplies (yeah, yeah2, yeah3);
					PlayerMessage hanging = new PlayerMessage ("Hanging with my dog");
						TheirMessage youHave = new TheirMessage ("You have a dog?!?");
						hanging.herReply = youHave;
							PlayerMessage yeahHaha = new PlayerMessage ("yeah, haha");
								yeahHaha.leftOnRead = true;
								yeahHaha.lorMessage = "Too boring.";
							PlayerMessage wannaSmash = new PlayerMessage ("Wanna smash?");
								wannaSmash.leftOnRead = true;
								wannaSmash.lorMessage = "Too forward.";
							PlayerMessage hisName = new PlayerMessage ("His name is Wonton");
								TheirMessage thatsSuch = new TheirMessage ("That's such a cute name!");
								hisName.herReply = thatsSuch;
									PlayerMessage wannaMeet = new PlayerMessage ("Wanna meet him?");
										TheirMessage idLove = new TheirMessage ("I'd love too! When?");
										wannaMeet.herReply = idLove;
											PlayerMessage thisFri = new PlayerMessage ("This Friday?");		
												TheirMessage date = new TheirMessage ("It's a date!");
												thisFri.herReply = date;
												date.youWin = true;
												date.gameOverMessage = "Game Win! \nBut could things have \ngone differently?";
												//@@@YOUWIN@@@
											PlayerMessage whenYou = new PlayerMessage ("When you give me \nthe SUCC");
												whenYou.leftOnRead = true;
												whenYou.lorMessage = "Too creepy.";
											PlayerMessage ohUh = new PlayerMessage ("Oh...uh, IDK?");
												ohUh.leftOnRead = true;
												ohUh.lorMessage = "You gotta go for it man!";
										idLove.setReplies (thisFri, whenYou,ohUh);
									PlayerMessage notAsCute = new PlayerMessage ("Not as cute as your \nsweet booty baby");
										notAsCute.leftOnRead = true;
										notAsCute.lorMessage = "Too forward.";
									PlayerMessage cuteLike = new PlayerMessage ("Cute like his owner \n;)");
										TheirMessage yourDad = new TheirMessage ("Your dad? hahaha");
										cuteLike.herReply = yourDad;
											PlayerMessage myDad = new PlayerMessage ("My dad is incredibly \nsexy");
												TheirMessage ohCool = new TheirMessage ("Oh...cool?");
												myDad.herReply = ohCool;
													PlayerMessage mmm = new PlayerMessage ("mmm yes daddy");
														mmm.leftOnRead = true;
														mmm.lorMessage = "Too creepy.";
													PlayerMessage mmm2 = new PlayerMessage ("mmm yes daddy");
														mmm2.leftOnRead = true;
														mmm2.lorMessage = "Too creepy.";
													PlayerMessage mmm3= new PlayerMessage ("mmm yes daddy");
														mmm3.leftOnRead = true;
														mmm3.lorMessage = "Too creepy.";
												ohCool.setReplies (mmm, mmm2, mmm3);
											PlayerMessage noMe = new PlayerMessage ("No, me you mouth \nbreather!");
												noMe.leftOnRead = true;
												noMe.lorMessage = "That was a little harsh.";
											PlayerMessage wellLike = new PlayerMessage ("Well, like father \nlike son");
												TheirMessage isSingle = new TheirMessage ("Is your dad single? \nlol");
												wellLike.herReply = isSingle;
													PlayerMessage number = new PlayerMessage ("Yeah, here's his number.");
														TheirMessage thanks = new TheirMessage ("Thanks!");
														number.herReply = thanks;
															thanks.gameOver = true;
															thanks.gameOverMessage = "She went on a date \nwith your dad instead.";
													PlayerMessage nahBut = new PlayerMessage ("Nah, but I've got a \nnewer model in \nstock");
														TheirMessage idLike = new TheirMessage ("I'd like to order one \nplease :)");
														nahBut.herReply = idLike;
															PlayerMessage outOf = new PlayerMessage ("Out of stock. Sorry");
																TheirMessage oh = new TheirMessage ("Oh...");
																outOf.herReply = oh;
																	oh.gameOver = true;
																	oh.gameOverMessage = "You were doing well until \nyou made that dumb choice.";
															PlayerMessage iDunno = new PlayerMessage ("I dunno... \nit's kinda pricey...");
																TheirMessage whatDo = new TheirMessage ("What do I have to pay?");
																iDunno.herReply = whatDo;
																	PlayerMessage booby = new PlayerMessage ("Booby pics");
																		TheirMessage seriously = new TheirMessage ("Seriously?");
																		booby.herReply = seriously;
																			PlayerMessage nahJustKid = new PlayerMessage ("Nah, just kidding");
																				nahJustKid.leftOnRead = true;
																				nahJustKid.lorMessage = "Smooth.";
																			PlayerMessage iWanna = new PlayerMessage ("I wanna see some \nboobs");
																				iWanna.leftOnRead = true;
																				iWanna.drakeJosh = true;
																				iWanna.altEnding = true;
																				iWanna.lorMessage = "I never thought that \nit'd be so simple.";
																				//picture of drake and josh
																			PlayerMessage ifYou = new PlayerMessage ("If you wanna");
																				TheirMessage iDontSend = new TheirMessage ("I don't send nudes...");
																				ifYou.herReply = iDontSend;
																					PlayerMessage right = new PlayerMessage ("Right, my bad");
																						right.leftOnRead = true;
																						right.lorMessage = "So close!";
																					PlayerMessage right2 = new PlayerMessage ("Right, my bad");
																						right2.leftOnRead = true;
																						right2.lorMessage = "So close!";
																					PlayerMessage right3 = new PlayerMessage ("Right, my bad");
																						TheirMessage exception = new TheirMessage ("...but I'll make an \nexception this time.");
																						right3.herReply = exception;
																						exception.youWin = true;
																						exception.gameOverMessage = "Game Win! \nYou scored the NUD35";
																						exception.nsfw = true;
																						//@@@YOUWIN@@@
																						//nudes5
																				iDontSend.setReplies (right, right2, right3);
																		seriously.setReplies (nahJustKid, iWanna, ifYou);
																	PlayerMessage dinner = new PlayerMessage ("Dinner with me?");
																		TheirMessage idLoveTo = new TheirMessage ("I'd love to!");
																		dinner.herReply = idLoveTo;
																		idLoveTo.youWin = true;
																		idLoveTo.gameOverMessage = "Game Win! \nBut could things have \ngone differently?";
																		//@@@YOUWIN@@@
																	PlayerMessage howAbout = new PlayerMessage ("How bout some fuk?");	
																		howAbout.leftOnRead = true;
																		howAbout.lorMessage = "Just a bit too bold.";
																whatDo.setReplies (booby, dinner, howAbout);
															PlayerMessage oneLarge = new PlayerMessage ("One large penis \ncoming right up!");	
																oneLarge.leftOnRead = true;
																oneLarge.lorMessage = "Jumped to conclusions a little there.";
														idLike.setReplies (outOf, iDunno, oneLarge);
													PlayerMessage hb = new PlayerMessage ("hb a 3 way?");
														hb.leftOnRead = true;
														hb.lorMessage = "Seriously dude? \nThree way with your dad?";
												isSingle.setReplies (number, nahBut, hb);
										yourDad.setReplies (myDad, noMe, wellLike);
								thatsSuch.setReplies (wannaMeet, notAsCute, cuteLike);
						youHave.setReplies (yeahHaha, wannaSmash, hisName);
				wyd.setReplies (creatingNudes, jChilling, hanging);

			PlayerMessage goodEvening = new PlayerMessage ("Good Evening M'lady");
				TheirMessage lolFormal = new TheirMessage ("lol, formal much?");
				goodEvening.herReply = lolFormal;
					PlayerMessage ofCourse = new PlayerMessage ("Of course, that is my \nnature as a \ngentleman.");
						TheirMessage youreA = new TheirMessage ("You're a gentleman, huh?");
						ofCourse.herReply = youreA;
							PlayerMessage tipsFedora = new PlayerMessage ("*tips fedora*");
								tipsFedora.leftOnRead = true;
								tipsFedora.lorMessage = "Nice guys finish last.";
							PlayerMessage butOfCourse = new PlayerMessage ("But of course madam. \nThere is no other way.");
								TheirMessage ohMyGod = new TheirMessage ("Oh my god...");
								butOfCourse.herReply = ohMyGod;
									PlayerMessage pft = new PlayerMessage ("Pft. God? I don't believe \nin silly fairy tales. I'm \nan enlightened Atheist");
										pft.leftOnRead = true;
										pft.lorMessage = "Friend zoned again.";
									PlayerMessage iDidnt = new PlayerMessage ("I didn't realize such \na beautiful maiden could \nbe of such simple mind");
										iDidnt.leftOnRead = true;
										iDidnt.lorMessage = "Nice guys finish last.";
									PlayerMessage iDont = new PlayerMessage ("I don't believe in god. \nHe's not nearly as cool \nor as strong as Goku");
										TheirMessage whoa = new TheirMessage ("Whoa. You like DBZ?");
										iDont.herReply = whoa;
											PlayerMessage over = new PlayerMessage ("IT'S OVER 9000!!");
												over.leftOnRead = true;
												over.lorMessage = "Was that joke ever funny?";
											PlayerMessage collectible = new PlayerMessage ("Of course, care to \nsee my collectible \nfigures?");
												TheirMessage omgDate = new TheirMessage ("OMG just date me \nalready stud!");
												collectible.herReply = omgDate;
												omgDate.youWin = true;
												omgDate.gameOverMessage = "Game Win! \nBut could things have \ngone differently?";
												//@@@YOUWIN@@@
											PlayerMessage kameha = new PlayerMessage ("KAMEHAMEHA!");
												kameha.leftOnRead = true;
												kameha.lorMessage = "You killed her! Try not to show \nyour true power levels next time.";
										whoa.setReplies (over, collectible, kameha);
								ohMyGod.setReplies (pft, iDidnt, iDont);
							PlayerMessage exceptWhen = new PlayerMessage ("Except when I fight with \nthe moves I learned from \nmy favorite animes");
								exceptWhen.leftOnRead = true;
								exceptWhen.lorMessage = "Friendzoned again.";
						youreA.setReplies (tipsFedora, butOfCourse, exceptWhen);
					PlayerMessage youEver = new PlayerMessage ("You ever seen that \nmeme?");
						TheirMessage ummIDont = new TheirMessage ("Um, I don't think so...");
						youEver.herReply = ummIDont;
							PlayerMessage thatsLame = new PlayerMessage ("That's lame");
								thatsLame.leftOnRead = true;
								thatsLame.lorMessage = "Don't trust the Internet.\nMemes don't get the girl.";
							PlayerMessage iCanSend = new PlayerMessage ("I can send you some \nmemes if you want");
								iCanSend.leftOnRead = true;
								iCanSend.lorMessage = "Don't trust the Internet.\nMemes don't get the girl.";
							PlayerMessage memesAreCool = new PlayerMessage ("Memes are cool");
								memesAreCool.leftOnRead = true;
								memesAreCool.lorMessage = "Don't trust the Internet.\nMemes don't get the girl.";
						ummIDont.setReplies (thatsLame, iCanSend, memesAreCool);
					PlayerMessage yeahALittle = new PlayerMessage ("Yeah, a little haha. I'm \npracticing for my \nShakespeare class.");
						TheirMessage oooWhat = new TheirMessage ("Ooo! What play are you \ndoing!?");
						yeahALittle.herReply = oooWhat;
							PlayerMessage romeo = new PlayerMessage ("I'm playing Romeo in \nRomero and Juliet");
								TheirMessage begone = new TheirMessage ("Begone filthy Montague");
								romeo.herReply = begone;
								begone.gameOver = true;	
								begone.gameOverMessage = "Shoot.\nYour crush is a Capulet.";
							PlayerMessage hamlet = new PlayerMessage ("I'm playing Hamlet in \nHamlet");
								TheirMessage hesThe = new TheirMessage ("He's the character who's \nreally indecisive, right?");
								hamlet.herReply = hesThe;
									PlayerMessage idk = new PlayerMessage ("idk");
										idk.leftOnRead = true;
										string idkLorm = "Gotta make a choice.\nWho knows, someone could die.";
										idk.lorMessage = idkLorm;
									PlayerMessage idk2 = new PlayerMessage ("idk");
										idk2.leftOnRead = true;
										idk2.lorMessage = idkLorm;
									PlayerMessage idk3 = new PlayerMessage ("idk");
										idk3.leftOnRead = true;
										idk3.lorMessage = idkLorm;
								hesThe.setReplies (idk, idk2, idk3);
							PlayerMessage king = new PlayerMessage ("I'm playing King Richard \nIII in King Richard III");
								TheirMessage conspire = new TheirMessage("Conspiring against the \nking in order to usurp the \nthrone really turns me on. \nCan I help?");
								king.herReply = conspire;
								conspire.youWin = true;
								conspire.gameOverMessage = "Game Win! \nBut could things have \ngone differently?";
								//@@@YOUWIN@@@
						oooWhat.setReplies (romeo, hamlet, king);

				lolFormal.setReplies (ofCourse, youEver, yeahALittle);

			PlayerMessage sendNudes = new PlayerMessage ("Send Nudes");
				TheirMessage umNo = new TheirMessage ("Ummm...no creep");
				sendNudes.herReply = umNo;
					PlayerMessage whateverPrude = new PlayerMessage ("Whatever, prude");
						whateverPrude.leftOnRead = true;
						whateverPrude.lorMessage = "Try not being a dick next time.";
					PlayerMessage kiddingSorry = new PlayerMessage ("Kidding! Sorry! \nBad Humor");
						TheirMessage howIsThat = new TheirMessage ("How is that supposed to \nbe funny?");
						kiddingSorry.herReply = howIsThat;
							PlayerMessage because = new PlayerMessage ("Because boobies make \nme giggle");
								because.leftOnRead = true;
								because.lorMessage = "Darn! She definitely seemed like \nshe was gonna send those nudes!";
							PlayerMessage becauseYour = new PlayerMessage ("Because your nudes \nwould be a good \nlaugh");
								becauseYour.leftOnRead = true;
								becauseYour.middleFinger = true;
								becauseYour.altEnding = true;
								//picture of a middle finger
								becauseYour.lorMessage = "Smooth.";
							PlayerMessage iJust = new PlayerMessage ("I just say stupid stuff \naround pretty people");
								TheirMessage whatNow = new TheirMessage ("What? Now you're tring \nto say I'm pretty?");
								iJust.herReply = whatNow;
									PlayerMessage whyElse = new PlayerMessage ("Why else would I make \nthat dumb joke?");
										TheirMessage greasy = new TheirMessage ("Because you're a greasy \nperv?");
										whyElse.herReply = greasy;
											PlayerMessage lol = new PlayerMessage ("lol, tru");
												lol.leftOnRead = true;
												string lolLorm = "She caught on to you.";
												lol.lorMessage = lolLorm;
											PlayerMessage lol2 = new PlayerMessage ("lol, tru");
												lol2.leftOnRead = true;
												lol2.lorMessage = lolLorm;
											PlayerMessage lol3 = new PlayerMessage("lol, tru");
												lol3.leftOnRead = true;
												lol3.lorMessage = lolLorm;
										greasy.setReplies (lol, lol2, lol3);
									PlayerMessage prettyFugly = new PlayerMessage ("Pretty fugly");
										prettyFugly.leftOnRead = true;
										prettyFugly.lorMessage = "Smooth.";
										prettyFugly.altEnding = true;
										prettyFugly.middleFinger = true;
										//picture of a middle finger
									PlayerMessage iThink = new PlayerMessage ("I think you've got \nnice melons");
										iThink.leftOnRead = true;
										iThink.lorMessage = "Try subtlety next time.";
								whatNow.setReplies (whyElse, prettyFugly, iThink);
						howIsThat.setReplies (because, becauseYour, iJust);
					PlayerMessage prettyPlease = new PlayerMessage ("Pretty please with \nnipples on top?");
						prettyPlease.leftOnRead = true;
						prettyPlease.lorMessage = "You sound like Buffalo Bill, \ncreepass.";
				umNo.setReplies (whateverPrude, kiddingSorry, prettyPlease);

		first.setReplies(hey, goodEvening, sendNudes);
		tm = first;
	}
}
