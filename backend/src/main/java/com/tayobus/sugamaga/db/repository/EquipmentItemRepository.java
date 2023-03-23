package com.tayobus.sugamaga.db.repository;

import com.tayobus.sugamaga.db.entity.EquipmentItem;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface EquipmentItemRepository extends JpaRepository<EquipmentItem, Integer> {
    List<EquipmentItem> findByEquipItemIdx(int equipItemIdx);
}
