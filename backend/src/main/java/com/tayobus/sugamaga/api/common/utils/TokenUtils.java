package com.tayobus.sugamaga.api.common.utils;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseAuthException;
import com.google.firebase.auth.FirebaseToken;
import com.tayobus.sugamaga.api.service.UserService;

public class TokenUtils {
    private static final TokenUtils instance = new TokenUtils();

    // public으로 열어서 객체 인스턴스가 필요하면 이 static 메소드를 통해 조회하도록 허용
    public static TokenUtils getInstance() {
        return instance;
    }

    private UserService userService;

    public Long TransUidToIdx(String idToken) throws FirebaseAuthException {
        return userService.getIdx(getUid(idToken));
    }

    public String getUid(String idToken) throws FirebaseAuthException {
        FirebaseToken decodedToken = FirebaseAuth.getInstance().verifyIdToken(idToken);

        return decodedToken.getUid();
    }
}
