package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.FirebaseAuthException;
import com.tayobus.sugamaga.api.common.utils.TokenUtils;
import com.tayobus.sugamaga.api.request.HistoryRequest;
import com.tayobus.sugamaga.api.service.GameService;
import com.tayobus.sugamaga.db.entity.ConsumableItem;
import com.tayobus.sugamaga.db.entity.DropTable;
import com.tayobus.sugamaga.db.entity.History;
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
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/game")
@Api(tags = "게임 API")
public class GameController {
    private final Logger logger = LoggerFactory.getLogger(GameController.class);

    private final static String SUCCESS = "Success";
    private final static String FAIL = "Fail";
    private final static String RESULT = "result";



    @Autowired
    private GameService gameService;

    @Operation(summary = "드랍 테이블 조회", description = "drop table get")
    @GetMapping("/droptable")
    public ResponseEntity<?> getDropTable(@RequestParam int tableIdx) {
        logger.info("get drop table");

        try {
            List<DropTable> dropTableList = new ArrayList<>();
            if (tableIdx == 0) {
                dropTableList = gameService.getDropTable();
            }
            else {
                dropTableList = gameService.getTargetDropTable(tableIdx);
            }

            Map<String, Object> result = new HashMap<>();
            result.put(RESULT, dropTableList);

            return new ResponseEntity<>(result, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "소비 아이템 조회", description = "consumeable item get")
    @GetMapping("/consume")
    public ResponseEntity<?> getConsumableItem(@RequestParam int consumableItemIdx) {
        logger.info("get ConsumableItem");

        try {
            List<ConsumableItem> consumableItemList = new ArrayList<>();
            if (consumableItemIdx == 0) {
                consumableItemList = gameService.getConsumableItem();
            }
            else {
                consumableItemList = gameService.getTargetConsumableItem(consumableItemIdx);
            }

            Map<String, Object> result = new HashMap<>();
            result.put(RESULT, consumableItemList);

            return new ResponseEntity<>(result, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "경기 기록 저장", description = "game result save")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "accessToken", value = "firebase 로그인 성공 후 " +
                    "발급 받은 accessToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @PostMapping("/history")
    public ResponseEntity<?> saveHistory(@RequestBody HistoryRequest historyRequest,
                                         HttpServletRequest httpServletRequest)
            throws FirebaseAuthException {
        logger.info("post history");

        try {
            String uid = TokenUtils.getInstance()
                    .getUid(httpServletRequest.getHeader("accessToken"));

            historyRequest.setUid(uid);

            gameService.saveHistory(historyRequest);

            return new ResponseEntity<>(SUCCESS, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "경기 기록 조회", description = "game result get")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "accessToken", value = "firebase 로그인 성공 후 " +
                    "발급 받은 accessToken",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping("/history")
    public ResponseEntity<?> getHistory(HttpServletRequest httpServletRequest)
            throws FirebaseAuthException {
        logger.info("get history");

        try {
            String uid = TokenUtils.getInstance()
                    .getUid(httpServletRequest.getHeader("accessToken"));

            List<History> historyList = gameService.getHistory(uid);
            Map<String, Object> result = new HashMap<>();
            result.put(RESULT, historyList);

            return new ResponseEntity<>(result, HttpStatus.OK);
        } catch (Exception e) {
            logger.info(e.toString());

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }


}
