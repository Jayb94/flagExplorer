import React, { useEffect, useState } from "react";
import Modal from "@mui/material/Modal";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";

const modalStyle = {
    position: "absolute",
    top: "50%", left: "50%",
    transform: "translate(-50%, -50%)",
    width: 300,
    bgcolor: "background.paper",
    border: "2px solid #000",
    boxShadow: 24,
    p: 4,
};

const CountryDetailsModal = ({countryDetails, modalOpen, handleCloseModal}) => {

    return (
        <Modal open={modalOpen} onClose={() => handleCloseModal()}>
        <Box sx={modalStyle}>
          {countryDetails && (
            <>
              <img
                src={countryDetails.flag}
                alt="Country Flag"
                style={{ width: "100%", marginBottom: "10px" }}
              />
              <table>
                <tr>
                  <td><Typography variant="h7">Country Name: </Typography></td><td><Typography variant="h6">{countryDetails.name}</Typography></td>
                </tr>
                <tr>
                  <td><Typography variant="h7">Population  : </Typography></td><td><Typography variant="h6">{countryDetails.population}</Typography></td>
                </tr>
                <tr>
                  <td><Typography variant="h7">Capital     : </Typography></td><td><Typography variant="h6">{countryDetails.capital}</Typography></td>
                </tr>
              </table>
            </>
          )}
        </Box>
      </Modal>
    );
}


export default CountryDetailsModal;