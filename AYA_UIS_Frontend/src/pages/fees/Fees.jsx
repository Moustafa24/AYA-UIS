import React, { useEffect, useState } from 'react';
import { FiDollarSign } from 'react-icons/fi';
import { feeService } from '../../services/otherServices';
import { toast } from 'react-toastify';

export default function Fees() {
  const [fees, setFees] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const load = async () => {
      try {
        const res = await feeService.getAll();
        setFees(res?.data || res || []);
      } catch (e) {
        toast.error('Failed to load fees');
      }
      setLoading(false);
    };
    load();
  }, []);

  if (loading)
    return (
      <div className="page-container">
        <div className="spinner" />
      </div>
    );

  return (
    <div className="page-container">
      <div className="page-header">
        <h1>
          <FiDollarSign style={{ marginRight: 8 }} />
          Department Fees
        </h1>
        <p>View and manage department fee structures</p>
      </div>

      {fees.length === 0 ? (
        <div className="card empty-state">
          <h3>No fee records found</h3>
        </div>
      ) : (
        <div style={{ display: 'grid', gap: 20 }}>
          {fees.map(df => (
            <div className="card" key={df.id}>
              <div
                style={{
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center',
                  marginBottom: 16,
                }}
              >
                <div>
                  <h3>{df.name}</h3>
                  <small style={{ color: 'var(--text-light)' }}>
                    Academic Year: {df.startYear} — {df.endYear}
                  </small>
                </div>
              </div>
              {df.fees?.length > 0 ? (
                <table>
                  <thead>
                    <tr>
                      <th>Fee Type</th>
                      <th>Amount</th>
                    </tr>
                  </thead>
                  <tbody>
                    {df.fees.map(f => (
                      <tr key={f.id}>
                        <td>{f.type}</td>
                        <td>
                          <strong>${f.amount?.toLocaleString()}</strong>
                        </td>
                      </tr>
                    ))}
                    <tr style={{ background: '#f7fafc' }}>
                      <td>
                        <strong>Total</strong>
                      </td>
                      <td>
                        <strong>
                          $
                          {df.fees
                            .reduce((s, f) => s + (f.amount || 0), 0)
                            .toLocaleString()}
                        </strong>
                      </td>
                    </tr>
                  </tbody>
                </table>
              ) : (
                <p style={{ color: 'var(--text-light)' }}>No fees configured</p>
              )}
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
