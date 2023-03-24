package com.tayobus.sugamaga.api.common.utils;

import com.google.firebase.auth.AuthErrorCode;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseAuthException;
import com.google.firebase.auth.FirebaseToken;
import com.tayobus.sugamaga.api.service.UserService;
import lombok.Data;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Component;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.reactive.function.client.WebClient;

import java.util.HashMap;
import java.util.Map;

@Component
public class TokenUtils {

    @Data
    static class tokenClass{
        String access_token;
        String refresh_token;
    }

    private final Logger logger = LoggerFactory.getLogger(TokenUtils.class);
    private final static TokenUtils instance = new TokenUtils();
    @Value("${key.firebase}")
    private String firebaseKey;

    // public으로 열어서 객체 인스턴스가 필요하면 이 static 메소드를 통해 조회하도록 허용
    public static TokenUtils getInstance() {
        return instance;
    }

    private UserService userService;

    public Long TransUidToIdx(String idToken, String reToken) throws FirebaseAuthException {
        return userService.getIdx((String) getUid(idToken));
    }

    public String getUid(String idToken) throws FirebaseAuthException {
        FirebaseToken decodedToken = FirebaseAuth.getInstance().verifyIdToken(idToken);

        logger.info("uid : " + decodedToken.getUid());

        return decodedToken.getUid();
    }


    public Map getNewUid(String idToken, String reToken) throws FirebaseAuthException {

        Map<String, String> result = new HashMap<>();

        try {
            FirebaseToken decodedToken = FirebaseAuth.getInstance().verifyIdToken(idToken);
            result.put("uid", decodedToken.getUid());

            return result;
        } catch (FirebaseAuthException e) {
            if (e.getAuthErrorCode() == AuthErrorCode.EXPIRED_ID_TOKEN) {
                // Token has been revoked. Inform the user to re-authenticate or signOut() the user.
                logger.info(e.getAuthErrorCode().toString());

                String newAccessToken = getRefreshToken(reToken);
                FirebaseToken decodedToken = FirebaseAuth.getInstance().verifyIdToken(newAccessToken);

                result.put("uid", decodedToken.getUid());
                result.put("accessToken", newAccessToken);

                return result;
            } else {
                // Token is invalid.
                logger.info("any catch");
                return null;
            }
        }
    }

    public String getRefreshToken(String reToken) {
        logger.info("firebaseKey : " + firebaseKey);

        String url = "https://securetoken.googleapis.com/v1/token?key="
                + "AIzaSyAbrcuxBD_VyzQDSlAJfxPUZOfsx8wfNy0";
        MultiValueMap<String, String> formData = new LinkedMultiValueMap<>();
        formData.add("grant_type", "refresh_token");
        formData.add("refresh_token", reToken);

        WebClient webClient = WebClient.builder()
                .baseUrl(url)
                .defaultHeader(HttpHeaders.CONTENT_TYPE, MediaType.APPLICATION_FORM_URLENCODED_VALUE)
                .build();

        tokenClass result = webClient.post()
                .bodyValue(formData)
                .retrieve()
                .bodyToMono(tokenClass.class)
                .block();

//        logger.info("result access token : " +  result.access_token);
//        logger.info("tokenClass : " + result.toString());

        return result.access_token;
    }

}
