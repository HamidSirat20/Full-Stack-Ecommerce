import React, { useState } from 'react';
import { Button, CircularProgress, Container, TextField } from '@mui/material';
import { deleteSignleProduct } from '../redux/reducers/productsReducer';
import useAppDispatch from '../hooks/useAppDispatch';

export default function DeleteProduct() {
  const dispatch = useAppDispatch();
  const [productId, setProductId] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleDelete = async () => {
    setLoading(true);
    setError('');

    try {
      await dispatch(deleteSignleProduct(productId));
      setLoading(false);
      setProductId('');
      setError('');
    } catch (error) {
      setLoading(false);
      setError('Error deleting product');
    }
  };

  return (
    <Container maxWidth="sm" sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <div>
        <h2>Delete Product</h2>
        {loading && <CircularProgress />}
        {error && <p>{error}</p>}
        <TextField
          label="Product ID"
          type="text"
          value={productId}
          onChange={(e) => setProductId(e.target.value)}
          fullWidth
          margin="normal"
        />
        <Button
          variant="contained"
          color="primary"
          onClick={handleDelete}
          disabled={loading || !productId}
        >
          Delete Product
        </Button>
      </div>
    </Container>
  );
}
