package com.tayobus.sugamaga.api.service;


import com.tayobus.sugamaga.api.request.SignRequest;
import com.tayobus.sugamaga.db.entity.User;
import com.tayobus.sugamaga.db.entity.UserCustom;
import com.tayobus.sugamaga.db.repository.UserCustomRepository;
import com.tayobus.sugamaga.db.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Random;

@Service
@RequiredArgsConstructor
public class SignService {
    private final Logger logger = LoggerFactory.getLogger(SignService.class);
    private final UserRepository userRepository;
    private final UserCustomRepository userCustomRepository;

    @Transactional(rollbackFor = Exception.class)
    public String signUp(SignRequest request) {
        try {
            userRepository.save(User.builder()
                    .email(request.getEmail())
                    .uid(request.getUid())
                    .nickname(request.getNickname())
                    .active(1)
                    .build());

            Random random = new Random();

            userCustomRepository.save(UserCustom.builder()
                    .body(random.nextInt(20))
                    .ac(random.nextInt(40))
                    .back(random.nextInt(7))
                    .eye(random.nextInt(12))
                    .hair(random.nextInt(14))
                    .hat(random.nextInt(15))
                    .head(random.nextInt(20))
                    .mouth(random.nextInt(12))
                    .eyebrow(random.nextInt(3))
                    .uid(request.getUid())
                    .build());

            return "Success";
        } catch (Exception e) {
            logger.warn("signUp fail - " + e);

            return "Fail";
        }
    }

}
