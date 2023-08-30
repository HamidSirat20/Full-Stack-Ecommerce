import React, { useState } from 'react';
import { InputAdornment, TextField } from '@mui/material';
import SearchIcon from '@mui/icons-material/Search';
import CloseIcon from '@mui/icons-material/Close';
import Product from '../types/Product';

interface SearchProductProps {
  placeholder: string;
  data: Product[];
}

function SearchProduct({ placeholder, data }: SearchProductProps) {
  const [filteredData, setFilteredData] = useState<Product[]>([]);
  const [wordEntered, setWordEntered] = useState<string>('');

  const handleFilter = (event: React.ChangeEvent<HTMLInputElement>) => {
    const searchWord = event.target.value;
    setWordEntered(searchWord);
    const newFilter = data.filter((value: Product) => {
      let product = value.title;
      return product.toLowerCase().includes(searchWord.toLowerCase());
    });
    setFilteredData(newFilter);
  };

  const clearInput = () => {
    setFilteredData([]);
    setWordEntered('');
  };

  return (
    <div className="SearchBar">
      <TextField
        fullWidth
        variant="outlined"
        placeholder={placeholder}
        value={wordEntered}
        onChange={handleFilter}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              {filteredData.length === 0 ? (
                <SearchIcon />
              ) : (
                <CloseIcon id="clearBtn" onClick={clearInput} />
              )}
            </InputAdornment>
          ),
        }}
      />

      {filteredData.length !== 0 && (
        <div className="SearchBar__dataResult">
          {filteredData.slice(0, 5).map((value, key) => (
            <div className="SearchBar__item" key={key}>
              <a className="SearchBar__dataItem" href={`/product/${value.id}`}>
                <p>{value.title}</p>
              </a>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default SearchProduct;
