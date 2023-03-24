package com.tayobus.sugamaga.config;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;

import javax.annotation.PostConstruct;
import java.io.FileInputStream;
import java.io.IOException;

@Configuration
public class FireBaseConfig {
    private final Logger logger = LoggerFactory.getLogger(FireBaseConfig.class);

    @PostConstruct
    public void init() throws IOException {
        ClassPathResource resource = new ClassPathResource("serviceAccountKey.json");
        String url = resource.getURL().toString();
        url = url.replace("file:/", "");
        url = url.replace("jar:", "");
        logger.info("url : " + url);

        try{
            FileInputStream serviceAccount =
                    new FileInputStream(url);

            FirebaseOptions options = new FirebaseOptions.Builder()
                    .setCredentials(GoogleCredentials.fromStream(serviceAccount))
                    .build();

            FirebaseApp.initializeApp(options);

            logger.info("firebase app name : " + FirebaseApp.getInstance().getName());
        }catch (Exception e){
            e.printStackTrace();
        }
    }
}
