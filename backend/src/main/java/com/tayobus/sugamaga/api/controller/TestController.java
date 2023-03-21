package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.AuthErrorCode;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseAuthException;
import com.google.firebase.auth.FirebaseToken;
import com.tayobus.sugamaga.api.request.TestTableRequest;
import com.tayobus.sugamaga.api.service.TestTableService;
import com.tayobus.sugamaga.db.entity.TestTable;
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
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/test")
@Api(tags = "테스트용 API")
public class TestController {
    private final Logger logger = LoggerFactory.getLogger(TestController.class);

    @Autowired
    private TestTableService testTableService;

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

    @Operation(summary = "get test", description = "GET METHOD 테스트 DB 연동")
    @GetMapping("/4")
    public ResponseEntity<?> test4(@RequestParam String strCol, @RequestParam int intCol) {
        logger.info("api test 4");

        TestTableRequest testTableRequest = new TestTableRequest();
        testTableRequest.setIntCol(intCol);
        testTableRequest.setStrCol(strCol);

        try {
            List<TestTable> testTable = testTableService.testGet(testTableRequest);
            Map<String, Object> res = new HashMap<>();
            res.put("result", testTable);

            return new ResponseEntity<>(res, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "post test", description = "Post METHOD 테스트 DB 연동")
    @PostMapping("/5")
    public ResponseEntity<?> test5(@RequestBody TestTableRequest testTableRequest, HttpServletRequest httpServletRequest) {
        logger.info("api test 5");

        try {
            testTableService.testPost(testTableRequest);

            return new ResponseEntity<>("Success", HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>("Fail", HttpStatus.BAD_REQUEST);
        }
    }

}
