package repository;

import model.User;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Properties;

/**
 * Created by rmihai on 27.03.2017.
 */
public class UserJdbcRepository {
    private JdbcUtils dbUtils;

    public UserJdbcRepository(Properties props){
        dbUtils=new JdbcUtils(props);
    }


    public User findOne(String id) {
        Connection con=dbUtils.getConnection();

        try(PreparedStatement preStmt=con.prepareStatement("select * from User where username=?")){
            preStmt.setString(1,id);
            try(ResultSet result=preStmt.executeQuery()) {
                if (result.next()) {
                    String username = result.getString("username");
                    String pass = result.getString("password");
                    User user = new User(username,pass);
                    return user;
                }
            }
        }catch (SQLException ex){
            System.out.println("Error DB "+ex);
        }
        return null;
    }

    public Iterable<User> findAll() {
        return new Iterable<User>() {
            @Override
            public Iterator<User> iterator() {
                return null;
            }
        };
    }
}
