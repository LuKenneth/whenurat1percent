using System;

public class TheirMessage {

	public PlayerMessage[] replies;

	public String message;

	public TheirMessage(String message) {
		this.message = message;
	}

	public void setReplies(PlayerMessage[] replies) {
		this.replies = replies;
	}

}