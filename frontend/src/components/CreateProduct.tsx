import { useState } from "react";
import { TextField, Button, CircularProgress, Container } from "@mui/material";
import { NewProduct } from "../types/NewProduct";
import { createNewProducts } from "../redux/reducers/productsReducer";
import useAppSelector from "../hooks/useAppSelector";
import useAppDispatch from "../hooks/useAppDispatch";

export default function CreateProduct() {
  const dispatch = useAppDispatch();
  const loading = useAppSelector((state) => state.productsReducer.loading);
  const error = useAppSelector((state) => state.productsReducer.error);

  const [newProduct, setNewProduct] = useState<NewProduct>({
    Title: "",
    Description: "",
    Price: 0,
    Inventory: 0,
    CategoryId: "",
  });

  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = event.target;
    setNewProduct((prevProduct) => ({
      ...prevProduct,
      [name]: value,
    }));
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    dispatch(createNewProducts(newProduct));
    setNewProduct({
      Title: "",
      Description: "",
      Price: 0,
      Inventory: 0,
      CategoryId: "",
    });
  };

  return (
    <Container
      maxWidth="sm"
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
      }}
    >
      <form onSubmit={handleSubmit} style={{ width: "100%" }}>
        <h2>Create New Product</h2>
        {loading && <CircularProgress />}
        {error && <p>{error}</p>}
        <TextField
          label="Title"
          type="text"
          name="Title"
          value={newProduct.Title}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Description"
          multiline
          rows={4}
          name="Description"
          value={newProduct.Description}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Price"
          type="number"
          name="Price"
          value={newProduct.Price}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Inventory"
          type="number"
          name="Inventory"
          value={newProduct.Inventory}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
        <TextField
          label="CategoryId"
          type="text"
          name="CategoryId"
          value={newProduct.CategoryId}
          onChange={handleInputChange}
          fullWidth
          margin="normal"
        />
        <Button
          type="submit"
          variant="contained"
          color="primary"
          // disabled={loading}
          fullWidth
        >
          Create Product
        </Button>
      </form>
    </Container>
  );
}
