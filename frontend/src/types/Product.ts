import { Category } from "./Category";
export interface Product {
  id: string;
  title: string;
  description: string;
  price: number;
  PictureUrl: string;
  category: Category;
  reviews: Review[];
}

export interface Review {
  id: string;
  comment: string;
  rating: number;
  productId: string;
  userId: string;
}

interface ProductDetails {
  id: string;
  title: string;
  description: string;
  price: number;
  image: string;
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
export default Product;
