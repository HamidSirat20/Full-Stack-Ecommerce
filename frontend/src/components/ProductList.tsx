import {
  Button,
  Card,
  CardContent,
  CardHeader,
  Container,
  Grid,
  Rating,
  TextareaAutosize,
  Typography,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { fetchAllProducts } from "../redux/reducers/productsReducer";
import Product from "../types/Product";

const ProductList = () => {
  const [search, setSearch] = useState("");
  const [products, setProducts] = useState<Product[]>([]);
  const allProducts = useAppSelector((state) => state.productsReducer.products);
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(
      fetchAllProducts({
        search:'',
        offset: 0,
        limit: 50,
      })
    );
  }, []);

  const handleSearch = () => {
    const filteredProducts = allProducts.filter(
      (product) => product.title.toLowerCase().includes(search.toLowerCase())
    );
    setProducts(filteredProducts);
  };

  const [query, setQuery] = useState({
    search: "",
    orderBy: "",
    orderByDescending: false,
    offset: 0,
    limit: 50,
  });
  const handleResetSearch = () => {
    setQuery({ ...query, search: "" });
    setSearch("");
    setProducts(allProducts);
  };
  const [expandedReviews, setExpandedReviews] = useState<{
    [key: string]: boolean;
  }>({});

  const handleExpandReviews = (productId: string) => {
    setExpandedReviews((prevExpandedReviews) => ({
      ...prevExpandedReviews,
      [productId]: !prevExpandedReviews[productId],
    }));
  };

  return (
    <>
      <Container maxWidth="lg">
        <div>
          <TextareaAutosize
            minRows={2}
            maxRows={4}
            placeholder="Search products"
            value={query.search}
            onChange={(e) => setQuery({ ...query, search: e.target.value })}
          />
          <Button variant="contained" onClick={handleSearch}>
            Search
          </Button>
          <Button variant="contained" onClick={handleResetSearch}>
            Reset
          </Button>
        </div>
        <Grid container spacing={3}>
          {products.map((product) => (
            <Grid item xs={12} sm={6} md={4} key={product.id}>
              <Card>
                <img
                  src={product.images[0]?.imageUrls}
                  alt={product.title}
                  style={{ width: "100%", height: "auto" }}
                />
                <CardHeader
                  title={product.title}
                  subheader={`$${product.price}`}
                />
                <CardContent>
                  <Typography variant="body2" color="textSecondary">
                    {product.description}
                  </Typography>
                  <div
                    style={{
                      backgroundColor: "lightgray",
                      padding: "10px",
                      marginTop: "10px",
                    }}
                  >
                    {product.reviews.length > 0 ? (
                      <div>
                        <Typography variant="subtitle1">Reviews:</Typography>
                        <Rating value={product.reviews[0].rating} readOnly />
                        <Typography variant="body2">
                          {product.reviews[0].comment}
                        </Typography>
                        {product.reviews.length > 1 && (
                          <>
                            {expandedReviews[product.id] ? (
                              <>
                                {product.reviews.slice(1).map((review) => (
                                  <div key={review.id}>
                                    <Rating value={review.rating} readOnly />
                                    <Typography variant="body2">
                                      {review.comment}
                                    </Typography>
                                  </div>
                                ))}
                                <Typography
                                  variant="body2"
                                  color="primary"
                                  style={{ cursor: "pointer" }}
                                  onClick={() =>
                                    handleExpandReviews(product.id)
                                  }
                                >
                                  See less
                                </Typography>
                              </>
                            ) : (
                              <Typography
                                variant="body2"
                                color="primary"
                                style={{ cursor: "pointer" }}
                                onClick={() => handleExpandReviews(product.id)}
                              >
                                See more
                              </Typography>
                            )}
                          </>
                        )}
                      </div>
                    ) : (
                      <Typography variant="body2">
                        No reviews available.
                      </Typography>
                    )}
                  </div>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </>
  );
};

export default ProductList;
