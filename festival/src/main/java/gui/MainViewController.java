package gui;

import controller.FestivalController;
import controller.LoginController;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;
import model.Bilet;
import model.Spectacol;
import model.User;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import repository.*;

import java.time.LocalDate;
import java.util.ArrayList;

/**
 * Created by rmihai on 28.03.2017.
 */
public class MainViewController {

    private FestivalController service;
    private ObservableList<Spectacol> modelAll;
    private ObservableList<Spectacol> modelSrc;

    @FXML
    private TableView<Spectacol> spectacoleTableView;
    @FXML
    private TableColumn idAColumn;
    @FXML
    private TableColumn artistAColumn;
    @FXML
    private TableColumn dataAColumn;
    @FXML
    private TableColumn locAColumn;
    @FXML
    private TableColumn disponibileAColumn;
    @FXML
    private TableColumn vanduteAColumn;
    @FXML
    private TableView<Spectacol> searchTableView;
    @FXML
    private TableColumn artistSrcColumn;
    @FXML
    private TableColumn locSrcColumn;
    @FXML
    private TableColumn oraSrcColumn;
    @FXML
    private TableColumn disponibileSrcColumn;
    @FXML
    private Button searchButton;
    @FXML
    private DatePicker searchDatePicker;
    @FXML
    private Button logoutButton;
    @FXML
    private Button cumparaButton;
    @FXML
    private TextField cantitateTextField;
    @FXML
    private TextField cumparatorTextField;

    public MainViewController() {
    }

    @FXML
    private void initialize() {
        idAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, Integer>("id"));
        locAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("locatie"));
        artistAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("numeArtist"));
        dataAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("date"));
        disponibileAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, Integer>("locuridisponibile"));
        vanduteAColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, Integer>("locuriocupate"));

        spectacoleTableView.setRowFactory(tv -> new TableRow<Spectacol>() {
            @Override
            public void updateItem(Spectacol item, boolean empty) {
                super.updateItem(item, empty);
                if (item == null) {
                    setStyle("");
                } else if (item.getLocuridisponibile() <= 0) {
                    setStyle("-fx-background-color: tomato;");
                } else {
                    setStyle("");
                }
            }
        });

        locSrcColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("locatie"));
        artistSrcColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("numeArtist"));
        oraSrcColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, String>("ora"));
        disponibileSrcColumn.setCellValueFactory(new PropertyValueFactory<Spectacol, Integer>("locuridisponibile"));

        searchTableView.setRowFactory(tv -> new TableRow<Spectacol>() {
            @Override
            public void updateItem(Spectacol item, boolean empty) {
                super.updateItem(item, empty);
                if (item == null) {
                    setStyle("");
                } else if (item.getLocuridisponibile() <= 0) {
                    setStyle("-fx-background-color: tomato;");
                } else {
                    setStyle("");
                }
            }
        });
    }

    public void setService(FestivalController service) {
        this.service = service;
        this.modelAll = FXCollections.observableArrayList(service.getAllSpectacole());
        spectacoleTableView.setItems(modelAll);

        this.modelSrc = FXCollections.observableArrayList(service.getSpectacole(""));
        searchTableView.setItems(modelSrc);
    }

    private void goToLogin() {
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(LoginViewController.class.getResource("/Views/loginWindow.fxml"));
        Parent root = null;
        try {
            root = loader.load();
        } catch (Exception e1) {
            e1.printStackTrace();
        }

        ApplicationContext context = new AnnotationConfigApplicationContext(RepoConfig.class);
        UserJdbcRepository userRepo = context.getBean(UserJdbcRepository.class);
        LoginController loginController = new LoginController(userRepo);

        Scene scene = new Scene(root);
        Stage mainStage = new Stage();
        mainStage.setTitle("Login");
        mainStage.setScene(scene);
        LoginViewController loginViewCtrl = loader.getController();
        loginViewCtrl.setService(loginController);
        mainStage.show();
    }

    @FXML
    private void handleLogout() {
        Stage stage = (Stage) logoutButton.getScene().getWindow();
        stage.close();
        this.goToLogin();
    }

    @FXML
    public void handleSearch() {
        LocalDate data = searchDatePicker.getValue();
        ArrayList<Spectacol> lista = (ArrayList<Spectacol>) service.getSpectacole(data.toString());
        if (lista.isEmpty() == true)
            showErrorMessage("Nu sunt spectacole in  aceea data !");
        else {
            modelSrc.setAll(lista);
        }
    }

    @FXML
    public void handleVanzare() {
        Spectacol s = searchTableView.getSelectionModel().getSelectedItem();
        Integer id = service.getNextIDBilet();
        String cumparator = cumparatorTextField.getText();
        if (!cumparator.equals("")) {
            try {
                Integer cantitate = Integer.parseInt(cantitateTextField.getText());
                Bilet b = new Bilet(id, s.getId(), cumparator, cantitate);
                Bilet saved = service.saveBilet(b);
                if (saved != null) {
                    update((ArrayList<Spectacol>) service.getAllSpectacole());
                } else {
                    showErrorMessage("Nu sunt locuri disponibile");
                }
            } catch (Exception e) {
                showErrorMessage(e.getMessage());
            }
        } else {
            showErrorMessage("Field gol!");
        }
    }

    public void update(ArrayList<Spectacol> lista) {
        ObservableList olist = FXCollections.observableArrayList(lista);
        modelAll.setAll(olist);
    }


    static void showErrorMessage(String text) {
        Alert message = new Alert(Alert.AlertType.ERROR);
        message.setTitle("Eroare");
        message.setContentText(text);
        message.showAndWait();
    }
}
