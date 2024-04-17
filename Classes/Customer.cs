namespace Rentals.Classes {
    public class Customer {
        private Int32 customerId;
        private String customerLastName;
        private String customerFirstName;
        private String customerPhone;
        private String customerEmail;
        private String customerNotes;

        public Customer(Int32 customerIdIn, String customerLastNameIn, String customerFirstNameIn, String customerPhoneIn, String customerEmailIn, String customerNotesIn) {
            this.customerId = customerIdIn;
            this.customerLastName = customerLastNameIn;
            this.customerFirstName = customerFirstNameIn;
            this.customerPhone = customerPhoneIn;
            this.customerEmail = customerEmailIn;
            this.customerNotes = customerNotesIn;
        }

        public Int32 getCustomerId() {
            return this.customerId;
        }

        public String getCustomerLastName() {
            return this.customerLastName;
        }

        public String getCustomerFirstName() {
            return this.customerFirstName;
        }

        public String getCustomerPhone() {
            return this.customerPhone;
        }

        public String getCustomerEmail() {
            return this.customerEmail;
        }

        public String getCustomerNotes() {
            return this.customerNotes;
        }

        public override String ToString() {
            return this.customerId.ToString() + ","
                + this.customerLastName + ","
                + this.customerFirstName + ","
                + this.customerPhone + ","
                + this.customerEmail + ","
                + this.customerNotes;
        }
    }
}
