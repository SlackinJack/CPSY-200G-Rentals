namespace Rentals.Classes {
    public class Category {

        private Int32 categoryId;
        private String categoryName;

        public Category(Int32 categoryIdIn, String categoryNameIn) {
            this.categoryId = categoryIdIn;
            this.categoryName = categoryNameIn;
        }

        public Int32 getCategoryId() {
            return this.categoryId;
        }

        public String getCategoryName() {
            return this.categoryName;
        }

        public override String ToString() {
            return this.categoryId + "," + this.categoryName;
        }
    }
}
