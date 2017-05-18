package sample;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.concurrent.Worker;
import javafx.concurrent.Worker.State;
import javafx.event.EventHandler;
import javafx.scene.Group;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.image.Image;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Region;
import javafx.scene.web.WebEngine;
import javafx.scene.web.WebView;
import javafx.stage.*;
import netscape.javascript.JSObject;

import java.net.URL;

//  w  w  w.j av a2s  . c  o m
public class Main extends Application {

    Stage stage;

    @Override
    public void start(final Stage stage) {
        this.stage = stage;
        stage.setWidth(400);
        stage.setHeight(500);
        Scene scene = new Scene(new Group());


        final WebView browser = new WebView();
        final WebEngine webEngine = browser.getEngine();

        ScrollPane scrollPane = new ScrollPane();
        scrollPane.setContent(browser);

        webEngine.getLoadWorker().stateProperty()
                .addListener(new ChangeListener<State>() {
                    @Override
                    public void changed(ObservableValue ov, State oldState, State newState) {

                        if (newState == Worker.State.SUCCEEDED) {
                            stage.setTitle(webEngine.getLocation());
                        }

                    }
                });

        URL url = this.getClass().getResource("/test.html");

        webEngine.load(url.toString());

        webEngine.getLoadWorker().stateProperty().addListener(new ChangeListener<Worker.State>() {
            @Override
            public void changed(ObservableValue<? extends Worker.State> observable, Worker.State oldValue, Worker.State newValue) {
                JSObject window = (JSObject) webEngine.executeScript("window");
                window.setMember("java", new Bridge());
                webEngine.executeScript("console.log = function(message) { java.log(message); }"); // Now where ever console.log is called in your html you will get a log in Java console
            }
        });

        scene.setRoot(scrollPane);

        stage.setScene(scene);
        stage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }

    public class Bridge {

        public void exit() {
            Platform.exit();
        }

        public void log(String text) {
            System.out.println("iets ");
            Alert alert = new Alert(Alert.AlertType.NONE);
            alert.initStyle(StageStyle.UNDECORATED);
            alert.setResizable(true);
            alert.setHeaderText(null);
            alert.getDialogPane().setPrefSize(1082, 816);

            stage.setMaxHeight(0);
            stage.setMaxWidth(0);
            stage.setX(Double.MAX_VALUE);

            WebView webView = new WebView();

            URL url = this.getClass().getResource("/notification.html");

            webView.getEngine().load(url.toString());
            webView.setPrefSize(1082, 816);
            alert.getDialogPane().setContent(webView);
            alert.getDialogPane().getButtonTypes().add(ButtonType.OK);

            alert.showAndWait();

            try {
                Thread.sleep(3000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

        }

    }

    public static void message(){
        System.out.println("tessst");
    }
}