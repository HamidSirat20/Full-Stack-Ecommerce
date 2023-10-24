import {
  Button,
  Card,
  CardActions,
  CardContent,
  CardHeader,
  Container,
  Grid,
  Paper,
  Rating,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { fetchSingleProduct } from "../redux/reducers/productsReducer";
import Product from "../types/Product";
import { addToCart } from "../redux/reducers/cartReducer";
import { useParams } from "react-router-dom";
import image1 from "../data/products/1.jpg";
import image2 from "../data/products/2.jpg";
import image3 from "../data/products/3.jpg";
import image4 from "../data/products/4.jpg";
import image5 from "../data/products/5.jpg";
import image6 from "../data/products/6.jpg";
import image7 from "../data/products/7.jpg";
import image8 from "../data/products/8.jpg";
import Review from "./Review";

const ProductDetails = () => {
  const dispatch = useAppDispatch();
  const [open, setOpen] = useState(false);

  const { productId } = useParams<{ productId: string }>();
  const prodId = String(productId);
  const product = useAppSelector((state) => state.productsReducer.products);

  const prod = [image1, image2, image3, image4, image5, image6, image7, image8];

  useEffect(() => {
    const fetchData = () => {
      return dispatch(fetchSingleProduct(prodId));
    };
    fetchData();
  }, [prodId, fetch]);

  const add = (product: Product) => {
    dispatch(addToCart(product));
  };
  const ratingValue = 3;

  return (
    <>
      <Container
        maxWidth="lg"
        style={{
          paddingBottom: "3rem",
          minHeight: "100vh",
          display: "flex",
          flexDirection: "column",
          marginTop: "5.3rem",
          marginBottom: "2.6rem",
        }}
      >
        <Grid container spacing={3} justifyContent="center">
          <Grid item xs={12} sm={6} md={6} key={product[0].id}>
            <Card elevation={3}>
              <img
                src={prod[0]}
                alt="images"
                style={{
                  width: "100%",
                  height: "200px",
                  objectFit: "cover",
                  backgroundSize: "contained",
                  textAlign: "center",
                }}
              />
              <CardHeader
                title={product[0].title}
                subheader={`$${product[0].price}`}
                subheaderTypographyProps={{
                  textAlign: "center",
                  variant: "h5",
                }}
                titleTypographyProps={{ variant: "h5", textAlign: "center" }}
              />
              <CardContent>
                <Typography variant="body2" color="textSecondary" paragraph>
                  {product[0].description}
                </Typography>
              </CardContent>
              {product[0].reviews.map((rev) => {
                return (
                  <Paper
                    key={rev.id}
                    style={{ padding: "16px", marginBottom: "16px" }}
                  >
                    <Typography variant="body2" color="textSecondary" paragraph>
                      {rev.comment}
                    </Typography>
                    <Rating value={rev.rating} readOnly />
                  </Paper>
                );
              })}

              <CardActions style={{ justifyContent: "center" }}>
                <Button onClick={(e) => setOpen(!open)}>
                  {open ? "Close Form" : "Add Review"}
                </Button>
                <Button
                  variant="outlined"
                  color="primary"
                  onClick={() => add(product[0])}
                >
                  Add To Cart
                </Button>
              </CardActions>
              <CardContent>{open ? <Review /> : ""}</CardContent>
            </Card>
          </Grid>
        </Grid>
      </Container>
    </>
  );
};

export default ProductDetails;
