import React from 'react';
import { render, screen } from '@testing-library/react';
import CountryDetailsModal from './components/CountryDetailsModal';

const mockCountryDetails = {
  flag: 'https://flagcdn.com/w320/za.png',
  name: 'Republic of South Africa',
  population: '59308690',
  capital: 'Pretoria, Bloemfontein, Cape Town',
};

describe('CountryDetailsModal Component', () => {
  it('renders the modal with country details when open', () => {
    render(
      <CountryDetailsModal
        countryDetails={mockCountryDetails}
        modalOpen={true}
        handleCloseModal={jest.fn()}
      />
    );

    expect(screen.getByAltText('Country Flag')).toBeInTheDocument();
    expect(screen.getByText(/Country Name:/i)).toBeInTheDocument();
    expect(screen.getByText(/Republic of South Africa/i)).toBeInTheDocument();
    expect(screen.getByText(/Population/i)).toBeInTheDocument();
    expect(screen.getByText(/59308690/i)).toBeInTheDocument();
    expect(screen.getByText(/Capital/i)).toBeInTheDocument();
    expect(screen.getByText(/Pretoria, Bloemfontein, Cape Town/i)).toBeInTheDocument();
  });

  it('does not render modal content when modalOpen is false', () => {
    render(
      <CountryDetailsModal
        countryDetails={mockCountryDetails}
        modalOpen={false}
        handleCloseModal={jest.fn()}
      />
    );

    expect(screen.queryByAltText('Country Flag')).not.toBeInTheDocument();
    expect(screen.queryByText(/Republic of South Africa/i)).not.toBeInTheDocument();
  });

});