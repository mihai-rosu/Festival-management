package repository;

import model.Spectacol;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

/**
 * Created by Mihai on 15.03.2017.
 */
public class SpectacolJdbcRepository implements IRepository<Integer, Spectacol> {
    private JdbcUtils dbUtils;

    public SpectacolJdbcRepository(Properties props) {
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public int size() {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select count(*) as [SIZE] from Spectacol")) {
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    return result.getInt("SIZE");
                }
            }
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }
        return 0;
    }

    @Override
    public void save(Spectacol entity) {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into Spectacol values (?,?,?,?,?,?,?)")) {
            preStmt.setInt(1, entity.getId());
            preStmt.setInt(2, entity.getIdA());
            preStmt.setString(3, entity.getDate());
            preStmt.setString(4, entity.getOra());
            preStmt.setString(5, entity.getLocatie());
            preStmt.setInt(6, entity.getLocuridisponibile());
            preStmt.setInt(7, entity.getLocuriocupate());
            int result = preStmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }

    }

    @Override
    public void delete(Integer id) {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("delete from Spectacol where ids=?")) {
            preStmt.setInt(1, id);
            int result = preStmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }
    }


    @Override
    public Spectacol findOne(Integer id) {
        Connection con = dbUtils.getConnection();

        try (PreparedStatement preStmt = con.prepareStatement("select * from Spectacol where ids=?")) {
            preStmt.setInt(1, id);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    Integer ids = result.getInt("ids");
                    Integer ida = result.getInt("ida");
                    String date = result.getString("date");
                    String ora = result.getString("ora");
                    String locatie = result.getString("locatie");
                    Integer nrlocuri = result.getInt("nrlocuri");
                    Integer locuriocupate = result.getInt("locuriocupate");
                    Spectacol spectacol = new Spectacol(ids, ida, date, ora, locatie, nrlocuri, locuriocupate);
                    return spectacol;
                }
            }
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }
        return null;
    }

    @Override
    public Iterable<Spectacol> findAll() {
        Connection con = dbUtils.getConnection();
        List<Spectacol> spectacole = new ArrayList<>();
        try (PreparedStatement preStmt = con.prepareStatement("select * from Spectacol")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    Integer ids = result.getInt("ids");
                    Integer ida = result.getInt("ida");
                    String date = result.getString("date");
                    String ora = result.getString("ora");
                    String locatie = result.getString("locatie");
                    Integer nrlocuri = result.getInt("nrlocuri");
                    Integer locuriocupate = result.getInt("locuriocupate");
                    Spectacol spectacol = new Spectacol(ids, ida, date, ora, locatie, nrlocuri, locuriocupate);
                    spectacole.add(spectacol);
                }
            }
        } catch (SQLException e) {
            System.out.println("Error DB " + e);
        }
        return spectacole;
    }

}
