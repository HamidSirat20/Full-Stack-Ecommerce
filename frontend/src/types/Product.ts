interface Product {
  id: string;
  title: string;
  description: string;
  price: number;
  images: [
    {
      id: string;
      imageUrls: string;
      productId: string;
    }
  ];
  category: Category;
  reviews: [
    {
      id: string;
      comment: string;
      rating: number;
      productId: string;
      userId: string;
    }
  ];
}

export interface Category {
  id: string;
  CategoryName: string;
  Image: string;
}
export default Product;
