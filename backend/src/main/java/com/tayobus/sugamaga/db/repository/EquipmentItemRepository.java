package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.EquipmentItem;
import org.springframework.data.jpa.repository.JpaRepository;

public interface EquipmentItemRepository extends JpaRepository<EquipmentItem, Integer> {
    EquipmentItem findByEquipItemIdx(int equipItemIdx);
}
