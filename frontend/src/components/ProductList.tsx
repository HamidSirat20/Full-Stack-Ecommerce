import {
  Button,
  Card,
  CardActions,
  CardHeader,
  CircularProgress,
  Container,
  Grid,
  Stack,
  Typography,
} from "@mui/material";
import { Link } from "react-router-dom";
import { useEffect } from "react";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { fetchAllProducts } from "../redux/reducers/productsReducer";
import Product from "../types/Product";
import { addToCart } from "../redux/reducers/cartReducer";
import image1 from "../data/products/1.jpg";
import image2 from "../data/products/2.jpg";
import image3 from "../data/products/3.jpg";
import image4 from "../data/products/4.jpg";
import image5 from "../data/products/5.jpg";
import image6 from "../data/products/6.jpg";
import image7 from "../data/products/7.jpg";
import image8 from "../data/products/8.jpg";
import image9 from "../data/products/9.jpg";
import image10 from "../data/products/10.jpg";
import image11 from "../data/products/11.jpg";
import image12 from "../data/products/12.jpg";
import image13 from "../data/products/13.jpg";
import image14 from "../data/products/14.jpg";

const ProductList = () => {
  const dispatch = useAppDispatch();
  const fetchProducts = useAppSelector(
    (state) => state.productsReducer.products
  );
  const loading = useAppSelector((state) => state.productsReducer.loading);

  const prod = [
    image1,
    image2,
    image3,
    image4,
    image5,
    image6,
    image7,
    image8,
    image9,
    image10,
    image11,
    image12,
    image13,
    image14,
  ];
  useEffect(() => {
    dispatch(fetchAllProducts({ limit: 30, offset: 0 }));
  }, []);

  const add = (product: Product) => {
    dispatch(addToCart(product));
  };

  return (
    <>
      <Container
        maxWidth="lg"
        style={{
          marginTop: "4.2rem",
          paddingBottom: "5rem",
          minHeight: "100vh",
          display: "flex",
          flexDirection: "column",
          justifyContent: fetchProducts.length === 0 ? "center" : "flex-start", // Center content if products list is empty
        }}
      >
        {loading ? (
          <Container
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              height: "100vh",
            }}
          >
            <Stack
              sx={{ color: "grey.500" }}
              spacing={3}
              direction="row"
              justifyContent="center"
            >
              <CircularProgress color="secondary" />
              <CircularProgress color="success" />
              <CircularProgress color="inherit" />
            </Stack>
          </Container>
        ) : (
          <Grid container spacing={3} justifyContent="center">
            {fetchProducts.map((product, index) => (
              <Grid item xs={12} sm={6} md={3} key={product.id}>
                <Card elevation={3}>
                  <img
                    src={prod[index]}
                    alt={product.title}
                    style={{
                      width: "100%",
                      height: "200px",
                      objectFit: "cover",
                      backgroundSize: "contained",
                    }}
                  />
                  <CardHeader
                    title={product.title}
                    subheader={`$ ${product.price}`}
                    subheaderTypographyProps={{
                      variant: "h5",
                      textAlign: "center",
                    }}
                    titleTypographyProps={{
                      variant: "h5",
                      textAlign: "center",
                    }}
                  />

                  <CardActions style={{ justifyContent: "center" }}>
                    <Link to={`/products/${product.id}`}>
                      <Button variant="text" sx={{ marginLeft: "0.5rem" }}>
                        View
                      </Button>
                    </Link>
                    <Button
                      variant="outlined"
                      color="primary"
                      onClick={() => add(product)}
                    >
                      Add To Cart
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        )}
      </Container>
    </>
  );
};

export default ProductList;
