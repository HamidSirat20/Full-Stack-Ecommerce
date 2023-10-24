export interface Order {
  shippingAddress: string;
  status: string;
  orderProducts: OrderProduct[];
}

export interface OrderProduct {
  productId: string;
  userId: string;
  amount: number;
}

export interface OrderRead {
  id: string;
  shippingAddress: string;
  status: string;
  userId: string;
  user: User;
  orderItems: OrderProduct[];
}

interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  avatar: string;
  role: string;
}
