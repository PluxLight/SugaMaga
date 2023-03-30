package com.tayobus.sugamaga.api.controller;

import com.tayobus.sugamaga.api.request.NicknameRequest;
import com.tayobus.sugamaga.api.request.UserCustomRequest;
import com.tayobus.sugamaga.api.service.UserService;
import com.tayobus.sugamaga.db.entity.UserCustom;
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
@RequestMapping("/api/user")
@Api(tags = "유저 API")
public class UserController {
    private final Logger logger = LoggerFactory.getLogger(UserController.class);

    private final static String SUCCESS = "Success";
    private final static String FAIL = "Fail";

    @Autowired
    public UserController(UserService userService) {
        this.userService = userService;
    }

    private final UserService userService;

    @Operation(summary = "유저 닉네임 조회", description = "헤더에 uid 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping(produces = "text/plain;charset=UTF-8")
    public ResponseEntity<?> getNickname(HttpServletRequest httpServletRequest) {
        logger.info("get Nickname");

        String uid = httpServletRequest.getHeader("uid");

        try {
            String Nickname = userService.getUserNickname(uid);

            return new ResponseEntity<>(Nickname, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>("Fail", HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "유저 닉네임 수정", description = "헤더에 uid 입력, 바디에 nickname 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @PutMapping()
    public ResponseEntity<?> putNickname(@RequestBody NicknameRequest nicknameRequest,
                                         HttpServletRequest httpServletRequest) {
        logger.info("put Nickname : " + nicknameRequest.getNickname());

        String uid = httpServletRequest.getHeader("uid");

        try {
            if (userService.putUserNickname(nicknameRequest.getNickname(), uid)) {
                return new ResponseEntity<>(SUCCESS, HttpStatus.OK);
            }
            else {
                return new ResponseEntity<>(FAIL, HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "유저 닉네임 사용가능 여부 조회", description = "유저 닉네임 입력")
    @GetMapping("/nickname")
    public ResponseEntity<?> searchNickname(@RequestParam String nickname) {
        logger.info("search nickname : " + nickname);

        try {
            if (userService.searchNickname(nickname)) {
                // 200
                return new ResponseEntity<>(SUCCESS, HttpStatus.OK);
            }
            else {
                // 403
                return new ResponseEntity<>(FAIL, HttpStatus.FORBIDDEN);
            }
        } catch (Exception e) {
            logger.info(e.toString());
            // 400
            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "유저 커스텀 조회", description = "헤더에 uid 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping(path = "/custom")
    public ResponseEntity<?> getCustom(HttpServletRequest httpServletRequest) {
        logger.info("get custom");

        String uid = httpServletRequest.getHeader("uid");

        try {
            UserCustom userCustom = userService.getUserCustom(uid);

            return new ResponseEntity<>(userCustom, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }


    @Operation(summary = "유저 커스텀 수정", description = "헤더에 uid 입력, 파라미터로 변경할 커스텀 입력")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @PutMapping(path = "/custom")
    public ResponseEntity<?> putCustom(@RequestBody UserCustomRequest userCustomRequest,
                                       HttpServletRequest httpServletRequest) {
        logger.info("put custom");

        String uid = httpServletRequest.getHeader("uid");

        try {
            userService.putUserCustom(uid, userCustomRequest);

            return new ResponseEntity<>(SUCCESS, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

}
