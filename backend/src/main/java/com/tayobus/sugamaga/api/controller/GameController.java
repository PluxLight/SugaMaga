package com.tayobus.sugamaga.api.controller;

import com.google.firebase.auth.FirebaseAuthException;
import com.tayobus.sugamaga.api.request.HistoryRequest;
import com.tayobus.sugamaga.api.service.GameService;
import com.tayobus.sugamaga.db.entity.*;
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

    private final GameService gameService;

    @Autowired
    public GameController(GameService gameService) {
        this.gameService = gameService;
    }

    @Operation(summary = "드랍 테이블 조회", description = "drop table get")
    @GetMapping(path = "/droptable")
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
            logger.info("get drop table error- " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "소비 아이템 조회", description = "consumeable item get")
    @GetMapping(path = "/consume")
    public ResponseEntity<?> getConsumableItem(@RequestParam int consumableItemIdx) {
        logger.info("get consumable item");

        try {
            if (consumableItemIdx == 0) {
                List<ConsumableItem> consumableItemList = gameService.getConsumableItem();
                Map<String, Object> result = new HashMap<>();
                result.put(RESULT, consumableItemList);

                return new ResponseEntity<>(result, HttpStatus.OK);
            }
            else {
                ConsumableItem consumableItem = gameService.getTargetConsumableItem(consumableItemIdx);

                return new ResponseEntity<>(consumableItem, HttpStatus.OK);
            }
        } catch (Exception e) {
            logger.info("get consumable item - " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "장비 아이템 조회", description = "equipment item get")
    @GetMapping(path="/equip")
    public ResponseEntity<?> getEquipmentItem(@RequestParam int equipmentItemIdx) {
        logger.info("get equipment item");

        try {
            if (equipmentItemIdx == 0) {
                List<EquipmentItem> equipmentItemList = gameService.getEquipmentItem();
                Map<String, Object> result = new HashMap<>();
                result.put(RESULT, equipmentItemList);

                return new ResponseEntity<>(result, HttpStatus.OK);
            }
            else {
                EquipmentItem equipmentItem = gameService.getTargetEquipmentItem(equipmentItemIdx);

                return new ResponseEntity<>(equipmentItem, HttpStatus.OK);
            }
        } catch (Exception e) {
            logger.info("get equip item error - " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "몬스터 조회", description = "monster get")
    @GetMapping(path = "/monster")
    public ResponseEntity<?> getMonster(@RequestParam int monsterIdx) {
        logger.info("get monster");

        try {
            if (monsterIdx == 0) {
                List<Monster> monsterList = gameService.getMonster();
                Map<String, Object> result = new HashMap<>();
                result.put(RESULT, monsterList);

                return new ResponseEntity<>(result, HttpStatus.OK);
            }
            else {
                Monster monster = gameService.getTargetMonster(monsterIdx);

                return new ResponseEntity<>(monster, HttpStatus.OK);
            }
        } catch (Exception e) {
            logger.info("get monster error - " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "경기 기록 저장", description = "game result save")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @PostMapping(path = "/history")
    public ResponseEntity<?> saveHistory(@RequestBody HistoryRequest historyRequest,
                                         HttpServletRequest httpServletRequest)
            throws FirebaseAuthException {
        logger.info("post history");

        try {
            historyRequest.setUid(httpServletRequest.getHeader("uid"));

            gameService.saveHistory(historyRequest);

            return new ResponseEntity<>(SUCCESS, HttpStatus.OK);
        } catch (Exception e) {
            logger.info("save history error - " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }

    @Operation(summary = "경기 기록 조회", description = "game result get")
    @ApiImplicitParams({
            @ApiImplicitParam(name = "uid", value = "firebase 로그인 성공 후 " +
                    "발급 받은 uid",
                    required = true, dataType = "String", paramType = "header")
    })
    @GetMapping(path = "/history")
    public ResponseEntity<?> getHistory(HttpServletRequest httpServletRequest)
            throws FirebaseAuthException {
        logger.info("get history");

        try {
            List<History> historyList = gameService.getHistory(httpServletRequest.getHeader("uid"));
            Map<String, Object> result = new HashMap<>();
            result.put(RESULT, historyList);

            return new ResponseEntity<>(result, HttpStatus.OK);
        } catch (Exception e) {
            logger.info("get history error - " + e.getMessage(), e);

            return new ResponseEntity<>(FAIL, HttpStatus.BAD_REQUEST);
        }
    }


}
