package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.AuthErrorCode;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseAuthException;
import com.google.firebase.auth.FirebaseToken;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiImplicitParam;
import io.swagger.annotations.ApiImplicitParams;
import io.swagger.v3.oas.annotations.Operation;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api/test")
@Api(tags = "테스트용 API")
public class TestController {
    private final Logger logger = LoggerFactory.getLogger(TestController.class);

    @Operation(summary = "get test", description = "GET METHOD 테스트")
    @GetMapping("/1")
    public ResponseEntity<?> test1() {
        logger.info("api test 1");

        return new ResponseEntity<>("api test 1 ", HttpStatus.OK);
    }

    @Operation(summary = "get firebase test", description = "GET METHOD 테스트")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "accessToken",
                    value = "firebase 로그인 성공 후 발급 받은 accessToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping("/2")
    public ResponseEntity<?> test2(HttpServletRequest httpServletRequest) throws FirebaseAuthException {
        logger.info("api test 2");

        String idToken = httpServletRequest.getHeader("accessToken");

        try {
            FirebaseToken decodedToken = FirebaseAuth.getInstance()
                    .verifyIdToken(idToken);

            String uid = decodedToken.getUid();

            return new ResponseEntity<>(uid, HttpStatus.OK);
        } catch (FirebaseAuthException e) {
            if (e.getAuthErrorCode() == AuthErrorCode.EXPIRED_ID_TOKEN) {
                logger.info(e.getAuthErrorCode().toString());
                return new ResponseEntity<>("EXPIRED_ID_TOKEN", HttpStatus.FORBIDDEN);
            } else {
                logger.info("any catch");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }
        }
    }

    @Operation(summary = "get test", description = "GET METHOD 테스트")
    @GetMapping("/3")
    public ResponseEntity<?> test3(@RequestParam String nickname, @RequestParam int level) {
        logger.info("api test 3");

        String result = String.format("nickname : %s | level : %d", nickname, level);
        Map<String, Object> res = new HashMap<>();
        res.put("nickname", nickname);
        res.put("level", String.valueOf(level));
        res.put("result", result);
        
        return new ResponseEntity<>(res, HttpStatus.OK);
    }

}
