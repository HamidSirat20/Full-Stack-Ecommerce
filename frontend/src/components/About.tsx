import React from 'react';
import { Container, Typography, Paper } from '@mui/material';

const About = () => {
  return (
    <Container maxWidth="md" sx={{ marginTop: '9rem', padding: '1rem' }}>
      <Paper elevation={3} sx={{ padding: '1rem', borderRadius: '10px' }}>
        <Typography variant="h4" gutterBottom>
          Welcome to PinnacleMall
        </Typography>
        <Typography variant="body1">
          At PinnacleMall, we bring you an extraordinary shopping experience
          where you can discover a wide range of high-quality products that
          cater to your needs. Our mission is to provide convenience and
          reliability, ensuring that your shopping journey is seamless and
          enjoyable. From the latest fashion trends to cutting-edge electronics,
          PinnacleMall offers a diverse selection that suits your lifestyle.
          With user-friendly navigation, secure transactions, and exceptional
          customer support, we strive to be your ultimate destination for all
          your shopping desires. Thank you for choosing PinnacleMall – your
          satisfaction is our priority!
        </Typography>
      </Paper>
    </Container>
  );
};

export default About;
