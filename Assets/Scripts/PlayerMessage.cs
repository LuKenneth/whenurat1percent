using System;

public class PlayerMessage {

	public String message;
	public bool leftOnRead = false;
	public TheirMessage herReply;
	public String lorMessage;
	public bool drakeJosh = false;
	public bool middleFinger = false;
	public bool altEnding = false;

	public PlayerMessage(String message) {
		this.message = message;
	}
}