import { useEffect, useRef, useState } from "react";
import { TextField, Button, Container, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";
import {
  createNewCategory,
  fetchAllCategories,
} from "../redux/reducers/categoryReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";

const CreateCategory = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [image, setImage] = useState("category.png");
  const [categoryName, setCategoryName] = useState("");
  const [message, setMessage] = useState("");

  const focRef = useRef<HTMLInputElement | null>(null);

  useEffect(() => {
    focRef.current?.focus();
    dispatch(fetchAllCategories());
  }, [dispatch]);

  const AddCategory = () => {
    if (categoryName === "") {
      setMessage("Please do leave empty");
    } else {
      dispatch(
        createNewCategory({
          categoryName: categoryName,
          image: image,
        })
      );
      setMessage("Category is created");
      navigate("/");
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
        <h2>Create New Category</h2>

        <TextField
          label="category"
          type="text"
          name="category"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          fullWidth
          margin="normal"
          inputRef={focRef}
        />
        <Typography color="red" sx={{ textAlign: "center" }}>
          {message}
        </Typography>
        <Container>
          <Button
            variant="contained"
            color="primary"
            fullWidth
            onClick={AddCategory}
          >
            Create Category
          </Button>
        </Container>
      </Container>
    </Container>
  );
};

export default CreateCategory;
