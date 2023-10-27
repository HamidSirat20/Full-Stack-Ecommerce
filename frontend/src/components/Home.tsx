import { Box, Button, CardMedia, Stack, Typography } from "@mui/material";
import { useEffect } from "react";
import { SvgIcon } from "@mui/material";

import {
  nextSlide,
  previouSlide,
  dotSlide,
} from "../redux/reducers/sliderReducer";
import useAppDispatch from "../hooks/useAppDispatch";
import useAppSelector from "../hooks/useAppSelector";
import { sliderData } from "../sliderData/sliderData";

const Home = () => {
  const slideIndex = useAppSelector((state) => state.sliderReducer.value);
  const dispatch = useAppDispatch();
  const handleNext = () => {
    dispatch(nextSlide(slideIndex + 1));
  };
  const handlePrev = () => {
    dispatch(previouSlide(slideIndex - 1));
  };
  useEffect(() => {
    const interval = setInterval(() => {
      dispatch(nextSlide(slideIndex + 1));
    }, 3000);
    return () => {
      clearInterval(interval);
    };
  }, [dispatch, slideIndex]);
  return (
    <>
      <Box
        position="relative"
        marginTop={1}
        marginBottom={1}
        style={{ height: "100vh", overflow: "hidden" }}
      >
        <Box>
          {sliderData.map((item, index) => {
            return (
              <Stack
                key={item.id}
                sx={{
                  opacity: parseInt(item.id) === slideIndex ? 1 : 0,
                  transitionDuration: "700ms",
                  transitionTimingFunction: "ease-in-out",
                  transform:
                    parseInt(item.id) === slideIndex
                      ? "scale(1)"
                      : "scale(0.95)",
                }}
              >
                <Stack>
                  {parseInt(item.id) === slideIndex && (
                    <CardMedia
                      sx={{
                        borderRadius: "10px 10px 0 0",
                        height: "100vh",
                        width: "100%",
                        objectFit: "cover",
                      }}
                      component="img"
                      image={item.img}
                      alt="product-image"
                    />
                  )}
                </Stack>
                <Box
                  position="absolute"
                  top={44}
                  left="25%"
                  right="25%"
                  marginLeft="auto"
                  marginRight="auto"
                  style={{
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    height: "100vh",
                  }}
                >
                  <Typography
                    variant="h1"
                    textAlign="center"
                    sx={{
                      color: "orange",
                      fontSize: ["1.5rem", "3rem", "4rem"],
                      fontFamily: "Inter",
                      fontWeight: "bold",
                      letterSpacing: "normal",
                      lineHeight: "1",
                    }}
                  >
                    {parseInt(item.id) === slideIndex && item.text}
                  </Typography>
                </Box>
              </Stack>
            );
          })}
        </Box>
        <Box display="flex" position="absolute" bottom={12} left="45%">
          {sliderData.map((dot, index) => {
            return (
              <Box
                sx={{
                  marginRight: "0.75rem",
                }}
                key={dot.id}
              >
                <Box
                  sx={{
                    backgroundColor: index === slideIndex ? "green" : "white",
                    borderRadius: "50%",
                    padding: "0.5rem",
                    cursor: "pointer",
                  }}
                  onClick={() => dispatch(dotSlide(index))}
                ></Box>
              </Box>
            );
          })}
        </Box>
        <Button
          sx={{
            position: "absolute",
            top: "50%",
            right: "6px",
            backgroundColor: "white",
            borderRadius: "50%",
            padding: "0.5rem",
            "&:hover": {
              backgroundColor: "green",
            },
          }}
          onClick={handleNext}
        >
          <SvgIcon
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            sx={{
              width: "1.5rem",
              height: "1.5rem",
            }}
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M8.25 4.5l7.5 7.5-7.5 7.5"
            />
          </SvgIcon>
        </Button>
        <Button
          sx={{
            position: "absolute",
            top: "50%",
            left: "6px",
            backgroundColor: "white",
            borderRadius: "50%",
            padding: "0.5rem",
            "&:hover": {
              backgroundColor: "green",
            },
          }}
          onClick={handlePrev}
        >
          <SvgIcon
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            sx={{
              width: "1.5rem",
              height: "1.5rem",
            }}
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M15.75 19.5L8.25 12l7.5-7.5"
            />
          </SvgIcon>
        </Button>
      </Box>
    </>
  );
};

export default Home;
