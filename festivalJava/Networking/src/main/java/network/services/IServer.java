package network.services;

import model.Bilet;
import model.Spectacol;
import model.User;

import java.util.List;

/**
 * Created by omg_s on 10-Apr-17.
 */
public interface IServer {
    void login(User user, IClient client) throws ComException;

    void logout(User user, IClient client) throws ComException;

    List<Spectacol> getAllSpectacole() throws ComException;

    List<Spectacol> getSpectacole(String data) throws ComException;

    Integer getNextIDBilet() throws ComException;

    Bilet saveBilet(Bilet b) throws ComException;
}
