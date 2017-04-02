import controller.LoginController;
import gui.LoginViewController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import repository.*;


public class Main extends Application {
    Parent root;

    @Override
    public void start(Stage primaryStage) {
        ApplicationContext context = new AnnotationConfigApplicationContext(RepoConfig.class);
        UserJdbcRepository urepo = context.getBean(UserJdbcRepository.class);
        LoginController loginController = new LoginController(urepo);

        //Load login window
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(Main.class.getResource("/Views/loginWindow.fxml"));
        try {
            root = loader.load();
        } catch (Exception e1) {
            e1.printStackTrace();
        }

        LoginViewController loginViewCtrl = loader.getController();
        loginViewCtrl.setService(loginController);

        Scene scene = new Scene(root);
        primaryStage.setTitle("Login");
        primaryStage.setScene(scene);
        primaryStage.show();
    }


    public static void main(String[] args) {

        launch(args);

        /*
        Normal
         */
//        Properties serverProps=new Properties();
//        try {
//            serverProps.load(new FileReader("bd.config"));
//            //System.setProperties(serverProps);
//
//            System.out.println("Properties set. ");
//            //System.getProperties().list(System.out);
//            serverProps.list(System.out);
//        } catch (IOException e) {
//            System.out.println("Cannot find bd.config "+e);
//        }
//        ArtistJdbcRepository repo=new ArtistJdbcRepository(serverProps);

        /*
        Spring XML
         */

//        ApplicationContext context = new ClassPathXmlApplicationContext("repoSpringXML.xml");
//        ArtistJdbcRepository arepo = context.getBean(ArtistJdbcRepository.class);

        /*
        Spring Java config
         */

    }
}
