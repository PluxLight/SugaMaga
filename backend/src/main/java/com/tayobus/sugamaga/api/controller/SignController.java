package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.FirebaseAuthException;
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

@RestController
@RequestMapping("/api/sign")
@Api(tags = "회원가입 API")
public class SignController {
    private final Logger logger = LoggerFactory.getLogger(SignController.class);

    private final SignService signService;
    @Autowired
    public SignController(SignService signService) {
        this.signService = signService;
    }

    @Operation(summary = "회원가입", description = "유저 Email, UID, nickname 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @PostMapping("/up")
    public ResponseEntity<?> signUp(@RequestBody SignRequest signRequest,
                                    HttpServletRequest httpServletRequest)
            throws FirebaseAuthException {
        logger.info("signUp - 호출, request - " + signRequest);

        signRequest.setUid(httpServletRequest.getHeader("uid"));

        if ( signService.signUp(signRequest).equals("Success") ) {
            logger.info("Sign Up Success!");

            return new ResponseEntity<>(HttpStatus.CREATED);
        }
        else {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

}
