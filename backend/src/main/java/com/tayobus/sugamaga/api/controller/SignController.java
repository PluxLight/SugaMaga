package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseAuthException;
import com.google.firebase.auth.FirebaseToken;
import com.tayobus.sugamaga.api.common.utils.TokenUtils;
import com.tayobus.sugamaga.api.request.SignRequest;
import com.tayobus.sugamaga.api.service.SignService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiImplicitParam;
import io.swagger.annotations.ApiImplicitParams;
import io.swagger.v3.oas.annotations.Operation;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import java.util.Map;

@RestController
@RequestMapping("/api/sign")
@Api(tags = "로그인 API")
public class SignController {
    private final Logger logger = LoggerFactory.getLogger(SignController.class);

    private final SignService signService;
    @Autowired
    public SignController(SignService signService) {
        this.signService = signService;
    }

    @Operation(summary = "회원가입", description = "유저 Email, UID, nickname 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "accessToken", value = "firebase 로그인 성공 후 발급 받은 accessToken", required = true, dataType = "String", paramType = "header")
    })
    @PostMapping("/up")
    public ResponseEntity<?> signUp(@RequestBody SignRequest signRequest, HttpServletRequest httpServletRequest) throws FirebaseAuthException {
        logger.info("signUp - 호출, request - " + signRequest);

        String idToken = httpServletRequest.getHeader("accessToken");
        FirebaseToken decodedToken = FirebaseAuth.getInstance().verifyIdToken(idToken);
        signRequest.setUid(decodedToken.getUid());

        if ( signService.signUp(signRequest).equals("Success") ) {
            logger.info("Sign Up Success!");

            return new ResponseEntity<>(HttpStatus.CREATED);
        }
        else {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "인증 유효 확인", description = "access, refresh token 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "accessToken", value = "firebase 로그인 성공 후 " +
                    "발급 받은 accessToken",
                    required = true, dataType = "String", paramType = "header"),
            @ApiImplicitParam(name = "refreshToken", value = "firebase 로그인 성공 후 " +
                    "발급 받은 refreshToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping("/verify")
    public ResponseEntity<?> verifyTest(HttpServletRequest httpServletRequest) throws FirebaseAuthException {
        logger.info("verify token");

        String idToken = httpServletRequest.getHeader("accessToken");
        logger.info("idToken : " + idToken);

        String reToken = httpServletRequest.getHeader("refreshToken");
        logger.info("reToken : " + reToken);

        try {
            Map<String, String> userMap =  TokenUtils.getInstance().getToken(idToken, reToken);

            return new ResponseEntity<>(userMap, HttpStatus.OK);
        } catch (Exception e) {
            logger.info("verifyTest error : " + e);

            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

}
