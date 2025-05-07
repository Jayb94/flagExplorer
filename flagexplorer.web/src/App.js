import React, { useEffect, useState } from "react";
import { DataGrid } from "@mui/x-data-grid";
import axios from "axios";
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

function App() {
  
  const [countries, setCountries] = useState([]);
  const [countryDetails, setCountryDetails] = useState(null);
  const [modalOpen, setModalOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    axios.get(process.env.REACT_APP_BASE_URL + "/Country/countries")
      .then((res) =>{
         setCountries(res.data.content || [])
         setIsLoading(false)
        })
      .catch((err) => console.error("Error:", err));
  }, []);
  
  const handleImageClick = (country) => {

    axios.get(process.env.REACT_APP_BASE_URL + "/Country/countries/" + country.name)
      .then((res) => setCountryDetails(res.data.content || {}))
      .catch((err) => console.error("Error:", err));

    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setCountryDetails(null);
    setModalOpen(false);
  };

  const columns = [
    {
      field: "flag",
      headerName: "Flag",
      width: 500,
      renderCell: (params) => (
        <img
          src={params.value}
          alt="flag"
          width={45}
          style={{ cursor: "pointer" }}
          onClick={() => handleImageClick(params.row)}
        />
      ),
    }
  ];

  return (
    <div style={{ height: 500, width: "40%", margin: "auto", marginTop: 40 }}>
      {!isLoading && <DataGrid
        rows={countries}
        columns={columns}
        getRowId={(row) => row.name}
        pageSize={20}
      />
      }
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
    </div>
  );
}

export default App;
