import { createSlice } from "@reduxjs/toolkit";

import { sliderData } from "../../sliderData/sliderData";

export interface Slider {
value: number
length: number
}
const initialState:Slider={
    value:0,
    length:sliderData.length,
}
export const sliderSlice = createSlice({
  name: "slider",
  initialState,
  reducers: {
    nextSlide(state,action){
        state.value = action.payload > state.length -1 ? 0 :action.payload
    },
    previouSlide(state,action){
        state.value = action.payload < 0 ? state.length -1 : action.payload
    },
    dotSlide(state,action){
        const slide = action.payload;
      state.value = slide;
    }
  },
});

const sliderReducer = sliderSlice.reducer
export const {nextSlide,previouSlide,dotSlide} = sliderSlice.actions
export default sliderReducer