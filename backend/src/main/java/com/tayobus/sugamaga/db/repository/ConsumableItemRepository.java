package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.ConsumableItem;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ConsumableItemRepository extends JpaRepository<ConsumableItem, Integer> {
    List<ConsumableItem> findByConsumableItemIdx(int consumableItemIdx);
}
