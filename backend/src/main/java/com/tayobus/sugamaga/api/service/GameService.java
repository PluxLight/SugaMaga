package com.tayobus.sugamaga.api.service;

import com.tayobus.sugamaga.api.request.HistoryRequest;
import com.tayobus.sugamaga.db.entity.ConsumableItem;
import com.tayobus.sugamaga.db.entity.DropTable;
import com.tayobus.sugamaga.db.entity.EquipmentItem;
import com.tayobus.sugamaga.db.entity.History;
import com.tayobus.sugamaga.db.repository.ConsumableItemRepository;
import com.tayobus.sugamaga.db.repository.DropTableRepository;
import com.tayobus.sugamaga.db.repository.EquipmentItemRepository;
import com.tayobus.sugamaga.db.repository.HistoryRepository;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class GameService {
    private final Logger logger = LoggerFactory.getLogger(GameService.class);
    private final DropTableRepository dropTableRepository;
    private final HistoryRepository historyRepository;
    private final ConsumableItemRepository consumableItemRepository;
    private final EquipmentItemRepository equipmentItemRepository;

    public List<DropTable> getDropTable() {
        logger.info("get drop table list");

        return dropTableRepository.findAll();
    }

    public List<DropTable> getTargetDropTable(int tableIdx) {
        logger.info("get Target drop table list");

        return dropTableRepository.findByTableIdx(tableIdx);
    }

    public List<ConsumableItem> getConsumableItem() {
        logger.info("get consumable Item list");

        return consumableItemRepository.findAll();
    }

    public List<ConsumableItem> getTargetConsumableItem(int consumableItemIdx) {
        logger.info("get target consumable Item list");

        return consumableItemRepository.findByConsumableItemIdx(consumableItemIdx);
    }

    public List<EquipmentItem> getEquipmentItem() {
        logger.info("get equipment item list");

        return equipmentItemRepository.findAll();
    }

    public List<EquipmentItem> getTargetEquipmentItem(int equipmentItemIdx) {
        logger.info("get target equipment item list");

        return equipmentItemRepository.findByEquipItemIdx(equipmentItemIdx);
    }

    public void saveHistory(HistoryRequest request) {
        historyRepository.save(History.builder()
                        .gameRoomIdx(request.getGameRoomIdx())
                        .uid(request.getUid())
                        .resultRank(request.getResultRank())
                        .resultKill(request.getResultKill())
                        .mapIdx(request.getMapIdx())
                .build());
    }

    public List<History> getHistory(String uid) {
        logger.info("get Target drop table list");

        return historyRepository.findAllByUid(uid);
    }

}
