namespace Rentals.Classes {
    public class Rental {

        private Int32 rentalId;
        private DateTime rentalDate;
        private Int32 rentalCustomerId;
        private Int32 rentalEquipmentId;
        private DateTime rentalActualDate;
        private DateTime rentalReturnDate;
        private Double rentalCostFinal;

        public Rental(
                Int32 rentalIdIn,
                DateTime rentalDateIn,
                Int32 rentalCustomerIdIn,
                Int32 rentalEquipmentIdIn,
                DateTime rentalActualDateIn,
                DateTime rentalReturnDateIn,
                Double rentalCostFinalIn
            ) {
            this.rentalId = rentalIdIn;
            this.rentalDate = rentalDateIn;
            this.rentalCustomerId = rentalCustomerIdIn;
            this.rentalEquipmentId = rentalEquipmentIdIn;
            this.rentalActualDate = rentalActualDateIn;
            this.rentalReturnDate = rentalReturnDateIn;
            this.rentalCostFinal = rentalCostFinalIn;
        }

        public Int32 getRentalId() {
            return this.rentalId;
        }

        public DateTime getRentalDate() {
            return this.rentalDate;
        }

        public Int32 getRentalCustomerId() {
            return this.rentalCustomerId;
        }

        public Int32 getRentalEquipmentId() {
            return this.rentalEquipmentId;
        }

        public DateTime getRentalActualDate() {
            return this.rentalActualDate;
        }

        public DateTime getRentalReturnDate() {
            return this.rentalReturnDate;
        }

        public Double getRentalCostFinal() {
            return this.rentalCostFinal;
        }

        public override string ToString() {
            return this.rentalId.ToString() + "," +
                this.rentalDate.ToString("M/d/yyyy") + "," +
                this.rentalCustomerId.ToString() + "," +
                this.rentalEquipmentId.ToString() + "," +
                this.rentalActualDate.ToString("M/d/yyyy") + "," +
                this.rentalReturnDate.ToString("M/d/yyyy") + "," +
                this.rentalCostFinal.ToString();

        }
    }
}
