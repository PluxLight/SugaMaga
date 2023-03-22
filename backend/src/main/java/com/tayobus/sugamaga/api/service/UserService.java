package com.tayobus.sugamaga.api.service;

import com.tayobus.sugamaga.api.request.UserCustomRequest;
import com.tayobus.sugamaga.db.entity.UserCustom;
import com.tayobus.sugamaga.db.repository.UserCustomRepository;
import com.tayobus.sugamaga.db.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class UserService {
    private final Logger logger = LoggerFactory.getLogger(UserService.class);
    private final UserCustomRepository userCustomRepository;
    private final UserRepository userRepository;

    public long getIdx(String uid) {
        return userRepository.findByUid(uid).get().getUserIdx();
    }

    public UserCustom getUserCustom(String uid) {
        UserCustom userCustom = userCustomRepository.findByUid(uid);

        logger.info(userCustom.toString());

        return userCustom;
    }

    public void putUserCustom(String uid, UserCustomRequest userCustomRequest) {
        UserCustom userCustom = userCustomRepository.findByUid(uid);
        userCustom.setCap(userCustomRequest.getCap());
        userCustom.setHair(userCustomRequest.getHair());
        userCustom.setFace(userCustomRequest.getFace());
        userCustom.setEyes(userCustomRequest.getEyes());
        userCustom.setMouse(userCustomRequest.getMouse());
        userCustom.setBody(userCustomRequest.getBody());

        logger.info(userCustom.toString());

        userCustomRepository.save(userCustom);
    }

}
