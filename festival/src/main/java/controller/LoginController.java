package controller;

import model.User;
import repository.UserJdbcRepository;

/**
 * Created by rmihai on 28.03.2017.
 */
public class LoginController {
    private UserJdbcRepository userRepo;

    public LoginController(UserJdbcRepository userRepo){
        this.userRepo = userRepo;
    }

    public void login(String username,String pass) throws Exception{
        User userL= userRepo.findOne(username);
        if (userL == null || !userL.getPassword().equals(pass)){
            throw new Exception("Authentication failed.");
        }
    }
}
