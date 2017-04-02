package model;

/**
 * Created by rmihai on 27.03.2017.
 */
public class User implements HasId<String> {
    private String username;
    private String password;

    public User(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public String getId() {
        return username;
    }

    public void setId(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
