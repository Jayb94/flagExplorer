import React, { useEffect, useState } from "react";
import { DataGrid } from "@mui/x-data-grid";
import axios from "axios";
import CountryDetailsModal from "./components/CountryDetailsModal";

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
        <CountryDetailsModal countryDetails={countryDetails} modalOpen={modalOpen} handleCloseModal={handleCloseModal} />
    </div>
  );
}

export default App;
