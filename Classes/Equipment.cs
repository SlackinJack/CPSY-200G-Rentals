namespace Rentals.Classes {
    public class Equipment {
        private Int32 equipmentId;
        private Int32 equipmentCategory;
        private String equipmentName;
        private String equipmentDescription;
        private Double equipmentCostDaily;

        public Equipment(Int32 equipmentIdIn, Int32 equipmentCategoryIn, String equipmentNameIn, String equipmentDescriptionIn, Double equipmentCostDailyIn) {
            this.equipmentId = equipmentIdIn;
            this.equipmentCategory = equipmentCategoryIn;
            this.equipmentName = equipmentNameIn;
            this.equipmentDescription = equipmentDescriptionIn;
            this.equipmentCostDaily = equipmentCostDailyIn;
        }

        public Int32 getEquipmentId() {
            return this.equipmentId;
        }

        public Int32 getEquipmentCategory() {
            return this.equipmentCategory;
        }

        public String getEquipmentName() {
            return this.equipmentName;
        }

        public String getEquipmentDescription() {
            return this.equipmentDescription;
        }

        public Double getEquipmentCostDaily() {
            return this.equipmentCostDaily;
        }

        public override string ToString() {
            return this.equipmentId.ToString() + ","
                + this.equipmentCategory.ToString() + ","
                + this.equipmentName + ","
                + this.equipmentDescription + ","
                + this.equipmentCostDaily.ToString();
        }
    }
}
