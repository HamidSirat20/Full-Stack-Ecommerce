import { useEffect, useRef, useState } from "react";
import {
  TextField,
  Button,
  CircularProgress,
  Container,
  Rating,
  Stack,
  Typography,
} from "@mui/material";
import { useNavigate, useParams } from "react-router-dom";
import { fetchAllCategories } from "../redux/reducers/categoryReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { createReview } from "../redux/reducers/reviewReducer";
import { fetchUserProfile } from "../redux/reducers/loginReducer";

const Review = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const [comment, setComment] = useState("");
  const [rating, setRating] = useState<number | null>(2);
  const [prodId, setProductId] = useState("");
  const [userId, setUserID] = useState<string>("");

  const { userProfile } = useAppSelector((state) => state.loginReducer);

  const [message, setMessage] = useState("");

  const canSave = Boolean(comment) && Boolean(rating);

  const { productId } = useParams<{ productId: string }>();

  useEffect(() => {
    if (userProfile?.id !== null) {
      setUserID(userProfile?.id || "");
    } else {
      setMessage("You need to be signed in in order to write a review");
    }
    if (productId !== undefined) {
      setProductId(productId);
    } else {
      setMessage("You need to be signed in in order to write a review");
    }
  }, [userId, rating]);

  const focRef = useRef<HTMLInputElement | null>(null);

  const loading = useAppSelector((state) => state.productsReducer.loading);

  useEffect(() => {
    dispatch(fetchAllCategories());
    focRef.current?.focus();
  }, [dispatch]);

  const addReview = () => {
    if (comment === "" || rating === 0 || productId === "" || userId === "") {
      setMessage("Please enter all input or you are not logged in");
    } else {
      dispatch(
        createReview({
          comment: comment,
          rating: Number(rating),
          productId: prodId,
          userId: userId,
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
        <h2>Add Review</h2>
        {loading && <CircularProgress />}
        {message && <Typography color="red">{message}</Typography>}
        <TextField
          label="Comment"
          multiline
          rows={4}
          name="comment"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
          fullWidth
          margin="normal"
          inputRef={focRef}
        />
        <Stack spacing={1} sx={{ margin: "1.3rem 0rem" }}>
          <Rating
            name="rating"
            value={rating}
            onChange={(event, newValue) => {
              setRating(newValue);
            }}
          />
        </Stack>

        <Container>
          <Button
            variant="contained"
            color="primary"
            fullWidth
            onClick={addReview}
            disabled={!canSave}
          >
            Add Review
          </Button>
        </Container>
      </Container>
    </Container>
  );
};

export default Review;
