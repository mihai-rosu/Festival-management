package repository;

import model.Bilet;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

/**
 * Created by Mihai on 22.03.2017.
 */
public class BiletJdbcRepository implements IRepository<Integer, Bilet> {
    private JdbcUtils dbUtils;

    public BiletJdbcRepository(Properties props) {
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public int size() {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("select count(*) as [SIZE] from Bilet")) {
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
    public void save(Bilet entity) {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("insert into Bilet values (?,?,?,?)")) {
            preStmt.setInt(1, entity.getId());
            preStmt.setInt(2, entity.getIdS());
            preStmt.setString(3, entity.getCumparator());
            preStmt.setInt(4,entity.getCantitate());
            int result = preStmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }

    }

    @Override
    public void delete(Integer id) {
        Connection con = dbUtils.getConnection();
        try (PreparedStatement preStmt = con.prepareStatement("delete from Bilet where idb=?")) {
            preStmt.setInt(1, id);
            int result = preStmt.executeUpdate();
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }
    }


    @Override
    public Bilet findOne(Integer id) {
        Connection con = dbUtils.getConnection();

        try (PreparedStatement preStmt = con.prepareStatement("select * from Bilet where idb=?")) {
            preStmt.setInt(1, id);
            try (ResultSet result = preStmt.executeQuery()) {
                if (result.next()) {
                    Integer idb = result.getInt("idb");
                    Integer ids = result.getInt("ids");
                    String cumparator = result.getString("cumparator");
                    Integer cantitate = result.getInt("cantitate");
                    Bilet bilet = new Bilet(idb, ids, cumparator, cantitate);
                    return bilet;
                }
            }
        } catch (SQLException ex) {
            System.out.println("Error DB " + ex);
        }
        return null;
    }

    @Override
    public Iterable<Bilet> findAll() {
        Connection con = dbUtils.getConnection();
        List<Bilet> bilete = new ArrayList<>();
        try (PreparedStatement preStmt = con.prepareStatement("select * from Spectacol")) {
            try (ResultSet result = preStmt.executeQuery()) {
                while (result.next()) {
                    Integer idb = result.getInt("idb");
                    Integer ids = result.getInt("ids");
                    String cumparator = result.getString("cumparator");
                    Integer cantitate = result.getInt("cantitate");
                    Bilet bilet = new Bilet(idb, ids, cumparator, cantitate);
                    bilete.add(bilet);
                }
            }
        } catch (SQLException e) {
            System.out.println("Error DB " + e);
        }
        return bilete;
    }
}

