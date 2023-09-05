import SignUp from "./components/SignUp";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import NotFound from "./components/NotFound";
import LogIn from "./components/LogIn";
import ProductList from "./components/ProductList";
import Home from "./components/Home";
import About from "./components/About";
import HeaderFooter from "./pages/HeaderFooter";
import CreateProduct from "./components/CreateProduct";
import DeleteProduct from "./components/DeleteProduct";

const router = createBrowserRouter([
  {
    path: "/",
    element: <HeaderFooter />,
    errorElement: <NotFound />,
    children: [
      {
        path: "/",
        element: <Home />,
      },
      {
        path: "/signup",
        element: <SignUp />,
      },
      {
        path: "/login",
        element: <LogIn />,
      },
      {
        path: "/products",
        element: <ProductList />,
      },
      {
        path: "/products/add",
        element: <CreateProduct />,
      },
      {
        path: "/products/delete",
        element: <DeleteProduct />,
      },
      {
        path: "/about",
        element: <About />,
      },

      // {
      //   path: "/product/:id",
      //   element: <ProductDetail />
      // },
      // {
      //   path: "/order/success",
      //   element: <OrderSuccess />
      // },
      // {
      //   path: "/order/payment",
      //   element: <PaymentPage />
      // },
      // {
      //   path: "/account",
      //   element: <AccountSetting />
      // },
      // {
      //   path: "/cart",
      //   element: <CartPage />
      // },
      // {
      //   path: "/product/manage",
      //   element: <ManageProduct />
      // },
      // {
      //   path: "/your-orders",
      //   element: <ShipmentPage />
      // }
    ],
  },
]);

const App = () => {
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
};

export default App;
