using System;

public class TheirMessage {

	public PlayerMessage[] replies;
	public bool gameOver = false;
	public bool youWin = false;
	public String gameOverMessage;
	public String message;
	public bool nsfw = false;

	public TheirMessage(String message) {
		this.message = message;
	}

	public void setReplies(PlayerMessage[] replies) {
		this.replies = replies;
	}

	public void setReplies(PlayerMessage reply1, PlayerMessage reply2, PlayerMessage reply3) {
		replies = new PlayerMessage[3];
		replies [0] = reply1;
		replies [1] = reply2;
		replies [2] = reply3;
	}

}