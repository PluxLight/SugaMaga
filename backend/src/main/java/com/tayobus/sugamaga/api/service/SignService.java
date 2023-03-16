package com.tayobus.sugamaga.api.service;


import com.tayobus.sugamaga.api.request.SignRequest;
import com.tayobus.sugamaga.db.entity.User;
import com.tayobus.sugamaga.db.repository.SignRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class SignService {
    private final Logger logger = LoggerFactory.getLogger(SignService.class);
    private final SignRepository signRepository;

    public String signUp(SignRequest request) {
        try {
            signRepository.save(User.builder()
                    .email(request.getEmail())
                    .uid(request.getUid())
                    .nickname(request.getNickname())
                    .build());

            return "Success";
        } catch (Exception e) {
            logger.warn("signUp fail - " + e);

            return "Fail";
        }
    }

}
