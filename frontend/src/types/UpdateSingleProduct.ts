export interface UpdateSingleProduct {
  id: string;
  update?: {
    Title: string;
    Description: string;
    Price: number;
    Inventory: number;
    CategoryId: string;
  };
}
