package gui;

import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import repository.*;
import service.FestivalService;

/**
 * Created by rmihai on 28.03.2017.
 */
public class LoginViewController {

    @FXML
    private TextField userTextField;
    @FXML
    private PasswordField passwordTextField;
    @FXML
    private Button loginButton;

    private FestivalService service;

    public LoginViewController() {
    }

    @FXML
    private void initialize() {
    }

    public void setService(FestivalService service) {
        this.service = service;
    }

    public void createMainWindow(){
        Stage stage = (Stage) loginButton.getScene().getWindow();
        stage.close();
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(LoginViewController.class.getResource("/Views/mainWindow.fxml"));
        Parent root = null;
        try {
            root = loader.load();
        } catch (Exception e1) {
            e1.printStackTrace();
        }

        Scene scene = new Scene(root);
        Stage mainStage = new Stage();
        mainStage.setTitle("Festival");
        mainStage.setScene(scene);
        MainViewController mainViewCtrl = loader.getController();
        mainViewCtrl.setService(service);
        mainStage.show();
    }

    public void handleLogin() {
        try {
            String user = userTextField.getText();
            String pass = passwordTextField.getText();
            service.login(user, pass);
            this.createMainWindow();
        } catch (Exception e) {
            showErrorMessage(e.getMessage());
        }

    }

    static void showErrorMessage(String text) {
        Alert message = new Alert(Alert.AlertType.ERROR);
        message.setTitle("Mesaj eroare");
        message.setContentText(text);
        message.showAndWait();
    }
}
