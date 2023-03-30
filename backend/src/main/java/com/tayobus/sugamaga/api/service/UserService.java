package com.tayobus.sugamaga.api.service;

import com.tayobus.sugamaga.api.request.UserCustomRequest;
import com.tayobus.sugamaga.db.entity.User;
import com.tayobus.sugamaga.db.entity.UserCustom;
import com.tayobus.sugamaga.db.repository.UserCustomRepository;
import com.tayobus.sugamaga.db.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@RequiredArgsConstructor
public class UserService {
    private final Logger logger = LoggerFactory.getLogger(UserService.class);
    private final UserCustomRepository userCustomRepository;
    private final UserRepository userRepository;

    public long getIdx(String uid) {
        return userRepository.findByUid(uid).get().getUserIdx();
    }

    public String getUserNickname(String uid) {
        Optional<User> user = userRepository.findByUid(uid);

        if (user.isPresent()) {
            return user.get().getNickname();
        }
        else {
            return "Fail";
        }
    }

    public boolean putUserNickname(String userNickname, String uid) {
        Optional<User> user = userRepository.findByUid(uid);

        if (user.isPresent()) {
            user.get().setNickname(userNickname);
            userRepository.save(user.get());
            
            return true;
        }
        else {
            return false;
        }
    }

    public UserCustom getUserCustom(String uid) {
        UserCustom userCustom = userCustomRepository.findByUid(uid);

        logger.info(userCustom.toString());

        return userCustom;
    }

    public void putUserCustom(String uid, UserCustomRequest userCustomRequest) {
        UserCustom userCustom = userCustomRepository.findByUid(uid);
        userCustom.setBody(userCustomRequest.getBody());
        userCustom.setAc(userCustomRequest.getAc());
        userCustom.setBack(userCustomRequest.getBack());
        userCustom.setEye(userCustomRequest.getEye());
        userCustom.setHair(userCustomRequest.getHair());
        userCustom.setHat(userCustomRequest.getHat());
        userCustom.setHead(userCustomRequest.getHead());
        userCustom.setMouth(userCustomRequest.getMouth());
        userCustom.setEyebrow(userCustomRequest.getEyebrow());

        logger.info(userCustom.toString());

        userCustomRepository.save(userCustom);
    }

}
