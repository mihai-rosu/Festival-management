package service;

import model.Bilet;
import model.Spectacol;
import model.User;
import network.services.ComException;
import network.services.IClient;
import network.services.IServer;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Mihai on 25.04.2017.
 */
public class FestivalService implements IClient {
    private IServer server;
    private User user;

    public FestivalService(IServer server) {
        this.server = server;
    }

    public void logout() {
        try {
            server.logout(user, this);
        } catch (ComException e) {
            System.out.println("Logout error "+e);
        }
    }

    public void login(String nume, String pass) throws ComException {
        User userL= new User(nume,pass);
        server.login(userL,this);
        user=userL;
    }

    public List<Spectacol> getAllSpectacole(){
        List<Spectacol> specs = new ArrayList<>();
        try {
            specs = server.getAllSpectacole();
        }catch(ComException e){
            System.out.println(e.getMessage());
        }
        return specs;
    }

    public List<Spectacol> getSpectacole(String data){
        List<Spectacol> specs = new ArrayList<>();
        try {
            specs = server.getSpectacole(data);
        }catch(ComException e){
            System.out.println(e.getMessage());
        }
        return specs;
    }

    public Integer getNextIDBilet(){
        Integer id = -1;
        try{
            id = server.getNextIDBilet();
        }catch(ComException e){
            System.out.println(e.getMessage());
        }
        return id;
    }

    public Bilet saveBilet(Bilet b){
        Bilet bilet = null;
        try{
            bilet = server.saveBilet(b);
        }catch(ComException e){
            System.out.println(e.getMessage());
        }
        return b;
    }

    public void biletAdded(){
        System.out.println("notificat");
    }

}
