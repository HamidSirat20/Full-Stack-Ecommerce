import { useEffect, useRef, useState } from "react";
import {
  TextField,
  Button,
  CircularProgress,
  Container,
  MenuItem,
  Select,
  InputLabel,
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { createNewProducts } from "../redux/reducers/productsReducer";
import { fetchAllCategories } from "../redux/reducers/categoryReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";

const CreateProduct = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [price, setPrice] = useState("");
  const [inventory, setInventory] = useState("");
  const [categoryName, setCategoryName] = useState("");
  const [message, setMessage] = useState("");

  const focRef = useRef<HTMLInputElement | null>(null);

  const loading = useAppSelector((state) => state.productsReducer.loading);
  const error = useAppSelector((state) => state.productsReducer.error);
  const categories = useAppSelector((state) => state.categoryReducer.category);

  useEffect(() => {
    dispatch(fetchAllCategories());
    focRef.current?.focus();
  }, [dispatch]);

  const AddProduct = () => {
    if (
      title === "" ||
      description === "" ||
      price === "" ||
      inventory === "" ||
      categoryName === ""
    ) {
      setMessage("Please enter all input");
    } else {
      dispatch(
        createNewProducts({
          title: title,
          description: description,
          price: Number(price),
          inventory: Number(inventory),
          categoryName: categoryName,
          images: [],
        })
      );
      navigate("/products");
    }
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
      <Container style={{ width: "100%" }}>
        <h2>Create New Product</h2>
        {error && <p>{error}</p>}
        <TextField
          label="Title"
          type="text"
          name="title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          fullWidth
          margin="normal"
          inputRef={focRef}
        />
        <TextField
          label="Description"
          multiline
          rows={4}
          name="description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          fullWidth
          margin="normal"
        />
        <TextField
          label="Price"
          type="number"
          name="price"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          fullWidth
          margin="normal"
          inputProps={{ min: 1 }}
        />
        <TextField
          label="Inventory"
          type="number"
          name="inventory"
          value={inventory}
          onChange={(e) => setInventory(e.target.value)}
          fullWidth
          margin="normal"
          inputProps={{ min: 1 }}
        />

        <InputLabel htmlFor="category">Category: </InputLabel>
        <Select
          labelId="category-label"
          id="category"
          label="Category"
          margin="dense"
          fullWidth
          name="categoryName"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          style={{ marginBottom: "1.3rem" }}
        >
          <MenuItem value="">Choose the Category</MenuItem>
          {categories.map((category) => (
            <MenuItem key={category.id} value={category.categoryName}>
              {category.categoryName}
            </MenuItem>
          ))}
        </Select>

        <Container>
          <Button
            variant="contained"
            color="primary"
            fullWidth
            onClick={AddProduct}
          >
            Create Product
          </Button>
        </Container>
      </Container>
    </Container>
  );
};

export default CreateProduct;
