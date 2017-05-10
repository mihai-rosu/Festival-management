package network.services;

/**
 * Created by omg_s on 10-Apr-17.
 */
public class ComException extends Exception {
    public ComException() {
    }

    public ComException(String message) {
        super(message);
    }

    public ComException(String message, Throwable cause) {
        super(message, cause);
    }
}
